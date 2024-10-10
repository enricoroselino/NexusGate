using NexusGate.Modules.Forecast.Persistence;

namespace NexusGate.Modules.Forecast;

public class ForecastModule : IModule
{
    public void AddModule(IServiceCollection services)
    {
        services.AddScoped<IWeatherRepository, WeatherRepository>();
    }
}