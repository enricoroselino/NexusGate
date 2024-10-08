using NexusGate.Infrastructure;
using NexusGate.Shared;
using NexusGate.Shared.Abstractions;

namespace NexusGate.Configurations;

internal static class ModulesDiscoveryConfiguration
{
    public static IServiceCollection AddModulesDiscoveryConfiguration(this IServiceCollection services)
    {
        var modules = DiscoverClasses.Search<IModule>();
        
        foreach (var module in modules)
        {
            module.RegisterModule(services);
        }

        return services;
    }
}