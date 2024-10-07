using Carter;
using MediatR;
using NexusGate.Modules.Weather.Application.Queries;
using NexusGate.Modules.Weather.Domain;

namespace NexusGate.Modules.Weather.Endpoints;

public sealed class WeatherEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        const string groupName = "weather";
        var group = app.MapGroup(groupName);

        group.MapGet("/forecast", GetWeatherForecast)
            .Produces<List<WeatherForecast>>()
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .WithName(nameof(GetWeatherForecast));
    }

    private static async Task<IResult> GetWeatherForecast(ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(GetWeatherForecastQuery.Instance, cancellationToken);
        return Results.Ok(result);
    }
}