using NexusGate.Modules.Forecast;

namespace NexusGate.Configurations;

internal static class ModulesConfiguration
{
    public static IServiceCollection AddModulesConfiguration(this IServiceCollection services)
    {
        services.AddForecastModule();
        return services;
    }
}