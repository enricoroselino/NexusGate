using Mediator;
using NexusGate.Configurations.Behaviors;

namespace NexusGate.Configurations.Services;

internal static class MediatorConfiguration
{
    public static IServiceCollection AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddMediator(cfg => cfg.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}