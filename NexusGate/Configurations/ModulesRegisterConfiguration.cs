using NexusGate.Configurations.Helpers;
using NexusGate.Modules;

namespace NexusGate.Configurations;

internal static class ModulesRegisterConfiguration
{
    public static IServiceCollection AddModulesRegisterConfiguration(this IServiceCollection services)
    {
        var assemblies = AssemblyHelper.LoadRecursiveAssemblies();
        var instances = InstanceHelper.GetInstances<IModule>(assemblies);
        
        foreach (var instance in instances)
        {
            instance.AddModule(services);
        }
        
        return services;
    }
}