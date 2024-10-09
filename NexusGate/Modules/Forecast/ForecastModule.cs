using NexusGate.Modules.Forecast.Features.Weather.Persistence;

namespace NexusGate.Modules.Forecast;

public class ForecastModule : IModule
{
    public void AddModule(IServiceCollection services)
    {
        services.AddScoped<IWeatherRepository, WeatherRepository>();
    }
}