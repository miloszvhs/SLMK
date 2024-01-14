using Microsoft.Extensions.Logging;
using ORM.Contract.Interfaces;

namespace ORM.Services;

public class DatabaseUpgradeService : IDatabaseUpgradeService
{
    private IConnectionStringProvider _connectionStringProvider;
    private IUpgradeService _upgradeService;
    private ILogger<DatabaseUpgradeService> _logger;
    
    public DatabaseUpgradeService(IConnectionStringProvider connectionStringProvider,
        IUpgradeService upgradeService,
        ILogger<DatabaseUpgradeService> logger)
    {
        _connectionStringProvider = connectionStringProvider;
        _upgradeService = upgradeService;
        _logger = logger;
    }

    public void ExecuteUpgradesForEveryDatabase()
    {
        _logger.LogInformation("Executing upgrades for every database");   
        var databases = _connectionStringProvider.GetAllConnectionStrings();
        foreach (var database in databases)
        {
            _upgradeService.ExecuteUpgrades(database.Key);    
        }
        _logger.LogInformation("Finished executing upgrades for every database");
    }
}

public interface IDatabaseUpgradeService
{
    void ExecuteUpgradesForEveryDatabase();
}