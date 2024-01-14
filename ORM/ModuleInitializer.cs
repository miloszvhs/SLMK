using System.Reflection;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using ORM.Contract.Interfaces;
using ORM.Repositories;
using ORM.Services;
using ORM.Upgrades;

namespace ORM;

public class ModuleInitializer : IModuleInitializer, IModuleConfigure
{
    private static IUpgradeService _upgradeService;
    private static IDatabaseUpgradeService _databaseUpgradeService;
    
    public void Initialize(IServiceCollection services)
    {
        services.AddSingleton<ISqlExecutor, Upgrade1>();
        
        services.AddSingleton<IConnectionService, ConnectionService>();
        services.AddSingleton<IUpgradeService, UpgradeService>();
        services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
        services.AddSingleton<ISessionFactoryService, SessionFactoryService>();
        services.AddSingleton<IUpgradeRepository, UpgradeRepository>();
        services.AddSingleton<IDatabaseUpgradeService, DatabaseUpgradeService>();

        services.AddSingleton<ModuleConfigurationService>();
    }

    public void Configure(IServiceProvider serviceProvider)
    {
        _upgradeService = serviceProvider.GetService<IUpgradeService>();
        if(_upgradeService == null)
            throw new Exception("Upgrade service is null");

        
        var jobId = BackgroundJob.Schedule(() => ExecuteMainUpgrades(), TimeSpan.FromSeconds(2));
        
        _databaseUpgradeService = serviceProvider.GetService<IDatabaseUpgradeService>();
        if(_databaseUpgradeService == null)
            throw new Exception("Database upgrade service is null");
        BackgroundJob.ContinueJobWith(jobId, () => ExecuteDatabaseUpgrades());
    }
    
    public class MappingFromAssembly : IMappingFromAssembly
    {
        public Assembly Assembly => Assembly.GetExecutingAssembly();
    }

    public void ExecuteMainUpgrades()
    {
        _upgradeService.ExecuteMainUpgrades();
    }
    
    public void ExecuteDatabaseUpgrades()
    {
        _databaseUpgradeService.ExecuteUpgradesForEveryDatabase();
    }
}