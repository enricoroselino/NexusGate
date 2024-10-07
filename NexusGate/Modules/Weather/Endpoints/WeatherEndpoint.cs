using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexusGate.Configurations;
using NexusGate.Infrastructure.Configurations;
using NexusGate.Modules.Weather.Application.Queries;

namespace NexusGate.Modules.Weather.Endpoints;

public sealed class WeatherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        const string groupName = "weather";
        var group = app.MapGroup(groupName)
            .WithOpenApi();

        group.MapGet("/forecast", GetWeatherForecast)
            .WithName(nameof(GetWeatherForecast))
            .RequireRateLimiting(IpRateLimiter.Name);


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