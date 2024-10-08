using NexusGate.Shared.Abstractions;

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
        // scan all assemblies
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic);

        return assemblies.Distinct()
            .SelectMany(a => a.GetTypes())
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
    }
}