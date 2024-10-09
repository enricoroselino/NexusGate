using System.Reflection;

namespace NexusGate.Configurations.Helpers;

public static class InstanceHelper
{
    public static IEnumerable<T> GetInstances<T>(IEnumerable<Assembly> assemblies)
    {
        var instances = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x is { IsClass: true, IsAbstract: false } && typeof(T).IsAssignableFrom(x))
            .Select(Activator.CreateInstance)
            .Cast<T>();

        return instances;
    }
}