using System.Reflection;

namespace NexusGate.Configurations;

public static class MediatorConfiguration
{
    public static IServiceCollection AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            var assembly = Assembly.GetExecutingAssembly();
            cfg.RegisterServicesFromAssembly(assembly);
        });
        return services;
    }
}