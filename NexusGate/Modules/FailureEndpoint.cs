using System.Threading.RateLimiting;
using Carter;
using NexusGate.Shared.Constants;
using NexusGate.Shared.Exceptions;
using Sprache;

namespace NexusGate.Modules;

public class FailureEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        const string groupName = "failure";
        var group = app.MapGroup(groupName);

        group.MapGet("/internal", InternalServerError);
        group.MapGet("/limiter", TooManyRequests)
            .RequireRateLimiting(LimiterPolicyConstant.IpRateLimiter);
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