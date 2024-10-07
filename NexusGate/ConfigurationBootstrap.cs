using System.Reflection;
using NexusGate.Configurations;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddConfigurationsBootstrap(this IServiceCollection services, Assembly assembly)
    {
        // Endpoint Configurations
        services.AddEndpointConfiguration();
        services.AddSwaggerConfiguration();
        services.AddModulesDiscoveryConfiguration();

        // Service Configurations
        services.AddFluentValidationConfiguration(assembly);
        services.AddBackgroundJobConfiguration();
        services.AddMediatorConfiguration(assembly);

        // Infrastructure Configurations
        services.AddRateLimiterConfiguration();
        services.AddJsonWebTokenConfiguration();
        services.AddSerilogConfiguration();
        return services;
    }

    public static WebApplication UseConfigurationsBootstrap(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerConfiguration();
        }
        
        app.UseSerilogConfiguration();
        app.UseRateLimiterConfiguration();
        app.UseEndpointConfiguration();
        return app;
    }
}