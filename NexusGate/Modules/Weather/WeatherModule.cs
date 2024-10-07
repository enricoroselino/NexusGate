﻿using NexusGate.Modules.Weather.Repositories;

namespace NexusGate.Modules.Weather;

public class WeatherModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection builder)
    {
        RegisterRepositories(builder);
        RegisterServices(builder);
        return builder;
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IWeatherRepository, WeatherRepository>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        
    }
}