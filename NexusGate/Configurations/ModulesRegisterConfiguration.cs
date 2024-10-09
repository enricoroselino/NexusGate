using NexusGate.Configurations.Helpers;
using NexusGate.Modules;

namespace NexusGate.Configurations;

internal static class ModulesRegisterConfiguration
{
    public static IServiceCollection AddModulesRegisterConfiguration(this IServiceCollection services)
    {
        var assemblies = AssemblyHelper.LoadExecutedAssemblies();
        var modules = InstanceHelper.GetInstances<IModule>(assemblies);
        
        foreach (var module in modules)
        {
            module.AddModule(services);
        }
        
        return services;
    }
}