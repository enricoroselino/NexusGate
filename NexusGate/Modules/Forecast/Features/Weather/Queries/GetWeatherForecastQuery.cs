using NexusGate.Abstractions;
using NexusGate.Modules.Forecast.Domain;
using NexusGate.Modules.Forecast.Features.Weather.Persistence;

namespace NexusGate.Modules.Forecast.Features.Weather.Queries;

public class GetWeatherForecastQuery : IQuery<List<WeatherForecast>>
{
    public static readonly GetWeatherForecastQuery Instance = new();
}

public class GetWeatherForecastQueryHandler : IQueryHandler<GetWeatherForecastQuery, List<WeatherForecast>>
{
    private readonly IWeatherRepository _weatherRepository;

    public GetWeatherForecastQueryHandler(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async Task<List<WeatherForecast>> Handle(
        GetWeatherForecastQuery request,
        CancellationToken cancellationToken)
    {
        return await _weatherRepository.GetForecastAsync(cancellationToken);
    }
}