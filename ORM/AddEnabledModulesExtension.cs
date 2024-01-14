using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ORM.Contract.Interfaces;

namespace ORM;

public static class AddEnabledModulesExtension
{
    public static void AddEnabledModules(this IServiceCollection services)
    {
        var moduleInitializers = GetModuleInitializers();
        foreach (var moduleInitializer in moduleInitializers)
        {
            moduleInitializer.Initialize(services);
        }
    }

    private static IEnumerable<IModuleInitializer> GetModuleInitializers()
    {
        var moduleInitializers = new List<IModuleInitializer>();
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(Assembly.LoadFrom);
        
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            var moduleInitializerType = typeof(IModuleInitializer);
            var moduleInitializerTypes = types.Where(x => moduleInitializerType.IsAssignableFrom(x) && x.IsClass);
            foreach (var moduleInitializer in moduleInitializerTypes)
            {
                var moduleInitializerInstance = Activator.CreateInstance(moduleInitializer) as IModuleInitializer;
                moduleInitializers.Add(moduleInitializerInstance);
            }
        }

        return moduleInitializers;
    }
}