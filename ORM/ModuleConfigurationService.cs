using System.Collections;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ORM.Contract.Interfaces;

namespace ORM;

public class ModuleConfigurationService
{
    private readonly IServiceProvider _serviceProvider;

    public ModuleConfigurationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ConfigureModules()
    {
        var moduleConfigures = GetModuleConfigures(_serviceProvider);
        foreach (var moduleConfigure in moduleConfigures)
        {
            moduleConfigure.Configure(_serviceProvider);
        }
    }

    private static IEnumerable<IModuleConfigure> GetModuleConfigures(IServiceProvider serviceProvider)
    {
        var moduleConfigures = new List<IModuleConfigure>();
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(Assembly.LoadFrom);

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            var moduleConfigureType = typeof(IModuleConfigure);
            var moduleConfigureTypes = types.Where(x => moduleConfigureType.IsAssignableFrom(x) && x.IsClass);
            foreach (var moduleConfigure in moduleConfigureTypes)
            {
                var moduleConfigureInstance = Activator.CreateInstance(moduleConfigure) as IModuleConfigure;
                moduleConfigures.Add(moduleConfigureInstance);
            }
        }

        return moduleConfigures;
    }
}