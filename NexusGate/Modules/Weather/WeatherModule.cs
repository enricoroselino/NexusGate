﻿using NexusGate.Modules.Weather.Persistence;
using NexusGate.Shared.Abstractions;

namespace NexusGate.Modules.Weather;

public class WeatherModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection builder)
    {
        RegisterRepositories(builder);
        return builder;
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IWeatherRepository, WeatherRepository>();
    }
}