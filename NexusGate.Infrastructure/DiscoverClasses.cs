using System.Reflection;

namespace NexusGate.Infrastructure;

public static class DiscoverClasses
{
    private static List<Assembly> Assemblies() => AppDomain.CurrentDomain.GetAssemblies()
        .Distinct()
        .Where(a => !a.IsDynamic)
        .ToList();
        

    public static IEnumerable<T> Search<T>()
    {
        var classes = Assemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(T).IsAssignableFrom(x) &&
                        x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<T>();

        return classes;
    }
}