using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NexusGate.Infrastructure.Behaviors;

namespace NexusGate.Infrastructure.Configurations;

public static class MediatorConfiguration
{
    public static IServiceCollection AddMediatorConfiguration(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        return services;
    }
}