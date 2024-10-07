using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexusGate.Infrastructure.Constants;
using NexusGate.Modules.Weather.Application.Queries;
using NexusGate.Modules.Weather.Domain;

namespace NexusGate.Modules.Weather.Endpoints;

public sealed class WeatherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        const string groupName = "weather";
        var group = app.MapGroup(groupName)
            .WithOpenApi();

        group.MapGet("/forecast", GetWeatherForecast)
            .Produces<List<WeatherForecast>>()
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .WithName(nameof(GetWeatherForecast))
            .RequireRateLimiting(LimiterConstant.IpRateLimiter);
        
        group.MapGet("/failed", GetFailedResult);
    }

    private static async Task<IResult> GetWeatherForecast(ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(GetWeatherForecastQuery.Instance, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetFailedResult()
    {
        var details = new ProblemDetails();
        return await Task.FromResult(Results.BadRequest(details));
    }
}