using NexusGate.Abstractions;
using NexusGate.Modules.Forecast.Domain;
using NexusGate.Modules.Forecast.Persistence;

namespace NexusGate.Modules.Forecast.Application.Queries;

public record GetSingleWeatherForecastQuery : IQuery<WeatherForecast>;

public class GetWeatherForecastQueryHandler : IQueryHandler<GetSingleWeatherForecastQuery, WeatherForecast>
{
    private readonly IWeatherRepository _weatherRepository;

    public GetWeatherForecastQueryHandler(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async ValueTask<WeatherForecast> Handle(
        GetSingleWeatherForecastQuery request,
        CancellationToken cancellationToken)
    {
        return await _weatherRepository.GetSingleForecastAsync(cancellationToken);
    }
}