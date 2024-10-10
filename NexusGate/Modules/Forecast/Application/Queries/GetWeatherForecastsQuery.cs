using NexusGate.Abstractions;
using NexusGate.Modules.Forecast.Domain;
using NexusGate.Modules.Forecast.Persistence;

namespace NexusGate.Modules.Forecast.Application.Queries;

public record GetWeatherForecastsQuery(int Count) : IQuery<List<WeatherForecast>>;

public class GetWeatherForecastsHandler : IQueryHandler<GetWeatherForecastsQuery, List<WeatherForecast>>
{
    private readonly IWeatherRepository _weatherRepository;
    
    public GetWeatherForecastsHandler(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }
    
    public async ValueTask<List<WeatherForecast>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        return await _weatherRepository.GetForecastAsync(request.Count, cancellationToken);
    }
}