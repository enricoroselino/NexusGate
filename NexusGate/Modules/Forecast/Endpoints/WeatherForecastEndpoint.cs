using Carter;
using MediatR;
using NexusGate.Modules.Forecast.Domain;
using NexusGate.Modules.Forecast.Features.Weather.Queries;

namespace NexusGate.Modules.Forecast.Endpoints;

public partial class WeatherForecastEndpoint: ICarterModule
{
    public virtual void AddRoutes(IEndpointRouteBuilder app)
    {
        const string groupName = "weather";
        var group = app.MapGroup(groupName);

        group.MapGet("/forecast", GetWeatherForecast)
            .Produces<List<WeatherForecast>>()
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .WithName(nameof(GetWeatherForecast));
    }
}

public partial class WeatherForecastEndpoint
{
    private static async Task<IResult> GetWeatherForecast(ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(GetWeatherForecastQuery.Instance, cancellationToken);
        return await Task.FromResult(Results.Ok(result));
    }
}