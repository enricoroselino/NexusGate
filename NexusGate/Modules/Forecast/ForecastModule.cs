using NexusGate.Modules.Forecast.Features.Weather.Persistence;

namespace NexusGate.Modules.Forecast;

public static class ForecastModule
{
    public static IServiceCollection AddForecastModule(this IServiceCollection services)
    {
        services.AddScoped<IWeatherRepository, WeatherRepository>();
        return services;
    }
}