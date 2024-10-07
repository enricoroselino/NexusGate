using Asp.Versioning;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NexusGate.Infrastructure.Configurations;

public static class EndpointConfiguration
{
    public static IServiceCollection AddEndpointConfiguration(this IServiceCollection services)
    {
        services.AddEndpointVersioningConfiguration();
        services.AddCarter();
        return services;
    }
}