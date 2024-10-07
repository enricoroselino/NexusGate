using Carter;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate.Configurations;

public static class EndpointConfiguration
{
    public static IServiceCollection AddEndpointConfiguration(this IServiceCollection services)
    {
        services.AddEndpointVersioningConfiguration();
        services.AddCarter();
        return services;
    }
}