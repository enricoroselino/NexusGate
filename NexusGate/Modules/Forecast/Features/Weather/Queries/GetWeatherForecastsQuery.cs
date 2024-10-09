using NexusGate.Abstractions;
using NexusGate.Modules.Forecast.Domain;
using NexusGate.Modules.Forecast.Features.Weather.Persistence;

namespace NexusGate.Modules.Forecast.Features.Weather.Queries;

public record GetWeatherForecastsQuery(int Count) : IQuery<List<WeatherForecast>>;

public class GetWeatherForecastsHandler : IQueryHandler<GetWeatherForecastsQuery, List<WeatherForecast>>
{
    private readonly IWeatherRepository _weatherRepository;
    
    public GetWeatherForecastsHandler(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }
    
    public async Task<List<WeatherForecast>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        return await _weatherRepository.GetForecastAsync(request.Count, cancellationToken);
    }
}