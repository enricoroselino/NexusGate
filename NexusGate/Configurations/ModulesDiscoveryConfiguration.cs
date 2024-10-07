using NexusGate.Modules;

namespace NexusGate.Configurations;

internal static class ModulesDiscoveryConfiguration
{
    public static IServiceCollection AddModulesDiscoveryConfiguration(this IServiceCollection services)
    {
        var modules = DiscoverModules();
        foreach (var module in modules)
        {
            module.RegisterModule(services);
        }
 
        return services;
    }
    
    private static IEnumerable<IModule> DiscoverModules()
    {
        return typeof(IModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
    }
}