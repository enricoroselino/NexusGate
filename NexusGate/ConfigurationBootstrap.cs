using NexusGate.Configurations;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddConfigurationsBootstrap(this IServiceCollection services)
    {
        services.AddEndpointConfiguration();
        services.AddSwaggerConfiguration();
        services.AddModulesDiscoveryConfiguration();

        services.AddFluentValidationConfiguration();
        services.AddBackgroundJobConfiguration();
        services.AddMediatorConfiguration();

        services.AddRateLimiterConfiguration();
        services.AddJsonWebTokenConfiguration();
        return services;
    }
}