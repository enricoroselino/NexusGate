using Carter;
using Mediator;
using NexusGate.Constants;
using NexusGate.Modules.Forecast.Application.Queries;
using NexusGate.Modules.Forecast.Domain;

namespace NexusGate.Modules.Forecast;

public partial class WeatherForecastEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        const string groupName = "weather";
        var group = app.MapGroup(groupName)
            .RequireRateLimiting(LimiterPolicyConstant.IpRateLimiter);

        group.MapGet("/forecast", GetSingleWeatherForecast)
            .Produces<WeatherForecast>()
            .WithName(nameof(GetSingleWeatherForecast));

        group.MapGet("/forecast/{count:int}", GetWeatherForecast)
            .Produces<List<WeatherForecast>>()
            .WithName(nameof(GetWeatherForecast));
    }
}

public partial class WeatherForecastEndpoint
{
    private static async Task<IResult> GetSingleWeatherForecast(ISender mediator, CancellationToken cancellationToken)
    {
        var query = new GetSingleWeatherForecastQuery();
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetWeatherForecast(int count, ISender mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetWeatherForecastsQuery(Count: count);
        var result = await mediator.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}