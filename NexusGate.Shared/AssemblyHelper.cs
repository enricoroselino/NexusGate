using System.Reflection;

namespace NexusGate.Shared;

public static class AssemblyHelper
{
    public static List<Assembly> LoadRecursiveAssemblies()
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
    
    public static List<Assembly> LoadExecutedAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .FilterAssemblies();

        return assemblies;
    }

    private static List<Assembly> FilterAssemblies(this IEnumerable<Assembly> assemblies)
    {
        return assemblies.Where(a => !a.IsDynamic).ToList();
    }
}