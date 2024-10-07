using System.Threading.RateLimiting;
using Carter;
using NexusGate.Infrastructure.Constants;
using NexusGate.Infrastructure.Exceptions;
using Sprache;

namespace NexusGate.Modules;

public class FailureEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        const string groupName = "failure";
        var group = app.MapGroup(groupName)
            .WithOpenApi();

        group.MapGet("/internal", InternalServerError);
        group.MapGet("/limiter", TooManyRequests)
            .RequireRateLimiting(LimiterConstant.IpRateLimiter);
    }

    private static async Task<IResult> TooManyRequests()
    {
        return await Task.FromResult(Results.Ok());
    }

    private static void InternalServerError()
    {
        throw new InternalServerException("Internal server error occured");
    }
}