using NexusGate.Abstractions;
using NexusGate.Modules.Forecast.Domain;
using NexusGate.Modules.Forecast.Features.Weather.Persistence;

namespace NexusGate.Modules.Forecast.Features.Weather.Queries;

public record GetSingleWeatherForecastQuery : IQuery<WeatherForecast>;

public class GetWeatherForecastQueryHandler : IQueryHandler<GetSingleWeatherForecastQuery, WeatherForecast>
{
    private readonly IWeatherRepository _weatherRepository;

    public GetWeatherForecastQueryHandler(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async Task<WeatherForecast> Handle(
        GetSingleWeatherForecastQuery request,
        CancellationToken cancellationToken)
    {
        return await _weatherRepository.GetSingleForecastAsync(cancellationToken);
    }
}