using System.Reflection;
using NexusGate.Configurations;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddConfigurationsBootstrap(this IServiceCollection services, Assembly assembly)
    {
        services.AddEndpointConfiguration();
        services.AddSwaggerConfiguration();
        services.AddModulesDiscoveryConfiguration();

        services.AddFluentValidationConfiguration(assembly);
        services.AddBackgroundJobConfiguration();
        services.AddMediatorConfiguration(assembly);

        services.AddRateLimiterConfiguration();
        services.AddJsonWebTokenConfiguration();
        return services;
    }
}