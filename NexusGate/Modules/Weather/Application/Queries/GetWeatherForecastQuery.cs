using NexusGate.Modules.Weather.Domain;
using NexusGate.Modules.Weather.Persistence;
using NexusGate.Shared.Abstractions.CQRS;

namespace NexusGate.Modules.Weather.Application.Queries;

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