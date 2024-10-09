using System.Reflection;

namespace NexusGate.Configurations.Helpers;

public static class AssemblyHelper
{
    public static IEnumerable<Assembly> LoadRecursiveAssemblies()
    {
        var returnAssemblies = new List<Assembly>();
        var loadedAssemblies = new HashSet<string>();
        var assembliesToCheck = new Queue<Assembly>();
        
        var rootAssembly = Assembly.GetEntryAssembly();
        if (rootAssembly == null) return [];

        assembliesToCheck.Enqueue(rootAssembly);

        while (assembliesToCheck.Count != 0)
        {
            var assemblyToCheck = assembliesToCheck.Dequeue();

            foreach (var reference in assemblyToCheck.GetReferencedAssemblies())
            {
                var fullName = reference.FullName;
                if (loadedAssemblies.Contains(fullName)) continue;
                
                var assembly = Assembly.Load(reference);
                assembliesToCheck.Enqueue(assembly);
                loadedAssemblies.Add(fullName);
                returnAssemblies.Add(assembly);
            }
        }

        return returnAssemblies.FilterAssemblies();
    }
    
    public static IEnumerable<Assembly> LoadExecutedAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .FilterAssemblies();

        return assemblies;
    }

    private static IEnumerable<Assembly> FilterAssemblies(this IEnumerable<Assembly> assemblies)
    {
        return assemblies.Where(a => !a.IsDynamic);
    }
}