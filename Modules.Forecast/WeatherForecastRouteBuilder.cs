using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Forecast.Core.Application.Queries;
using Modules.Forecast.Core.Domain;
using NexusGate.Shared.Abstractions;

namespace Modules.Forecast;

public class WeatherForecastEndpoint
{
    protected WeatherForecastEndpoint()
    {
    }

    protected static async Task<IResult> GetWeatherForecast(ISender mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(GetWeatherForecastQuery.Instance, cancellationToken);
        return await Task.FromResult(Results.Ok(result));
    }
}

public class WeatherForecastRouteBuilder: WeatherForecastEndpoint, IEndpoint
{
    public virtual void AddRoutes(IEndpointRouteBuilder routeBuilder)
    {
        const string groupName = "weather";
        var group = routeBuilder.MapGroup(groupName);

        group.MapGet("/forecast", GetWeatherForecast)
            .Produces<List<WeatherForecast>>()
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .WithName(nameof(GetWeatherForecast));
    }
}