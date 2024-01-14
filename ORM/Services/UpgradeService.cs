using Microsoft.Extensions.Logging;
using NHibernate;
using ORM.Contract.Enums;
using ORM.Contract.Interfaces;
using ORM.Entities;
using ORM.Repositories;

namespace ORM.Services;

public class UpgradeService : IUpgradeService
{
    private readonly IEnumerable<ISqlExecutor> _upgrades;
    private readonly IConnectionService _connectionService;
    private readonly IUpgradeRepository _upgradeRepository;
    private readonly ILogger<UpgradeService> logger;
    
    public UpgradeService(IEnumerable<ISqlExecutor> upgrades, 
        IConnectionService connectionService,
        IUpgradeRepository upgradeRepository, 
        ILogger<UpgradeService> logger)
    {
        _upgrades = upgrades;
        _connectionService = connectionService;
        _upgradeRepository = upgradeRepository;
        this.logger = logger;
    }

    public void ExecuteUpgrades(DatabaseConnectionKeys databaseKey)
    {
        _connectionService.Commit(databaseKey.ToString(), session =>
        {
            UpdateMissingOrmUpgrades(session);
            ExecuteSpecificUpgrades(session, databaseKey);
        });
    }

    private void ExecuteSpecificUpgrades(ISession session, DatabaseConnectionKeys databaseKey)
    {
        var sql = $@"SELECT number FROM Upgrade WHERE connectionKey = '{databaseKey}'";
        var upgradeNumbers = session
            .CreateSQLQuery(sql)
            .List<int>();
        
        var missingUpgrades = _upgrades
            .Where(x => x.ConnectionKey == databaseKey)
            .Where(x => !upgradeNumbers.Contains(x.Number))
            .OrderBy(x => x.Number)
            .ToList();

        foreach (var upgrade in missingUpgrades)
        {
            logger.LogInformation($"Executing upgrade {upgrade.Number} for {upgrade.ConnectionKey}");
            _connectionService.Commit(databaseKey.ToString(), upgrade.Execute);
            _upgradeRepository.Save(session, new Upgrade() { 
                Number = upgrade.Number,
                ConnectionKey = upgrade.ConnectionKey 
            });
        }
    }

    public void ExecuteMainUpgrades()
    {
        _connectionService.Commit(DatabaseConnectionKeys.Main.ToString(), UpdateMissingOrmUpgrades);
    }

    private void UpdateMissingOrmUpgrades(ISession session)
    {
        if (!CheckIfUpgradeTableExists(session))
        {
            var upgradeTableSql = @"CREATE TABLE IF NOT EXISTS Upgrade 
            (
                Id SERIAL,
                Number INT NOT NULL,
                ConnectionKey TEXT NOT NULL,
                PRIMARY KEY (id)
            )";
            session
                .CreateSQLQuery(upgradeTableSql)
                .ExecuteUpdate();
        }
        
        var sql = $@"SELECT number FROM Upgrade WHERE connectionKey = '{DatabaseConnectionKeys.Orm}'";
        var ormUpgradeNumbers = session
            .CreateSQLQuery(sql)
            .List<int>();
    
        var ormUpgrades = _upgrades
            .Where(x => x.ConnectionKey == DatabaseConnectionKeys.Orm)
            .ToList();

       var missingUpgrades = ormUpgrades
            .Where(x => !ormUpgradeNumbers.Contains(x.Number))
            .OrderBy(x => x.Number)
            .ToList();

       foreach (var upgrade in missingUpgrades)
       {
           logger.LogInformation($"Executing upgrade {upgrade.Number} for {upgrade.ConnectionKey}");
           upgrade.Execute(session);
           _upgradeRepository.Save(session, new Upgrade() { 
               Number = upgrade.Number,
               ConnectionKey = upgrade.ConnectionKey 
           });
       }
    }
    
    private bool CheckIfUpgradeTableExists(ISession session)
    {
        var sql = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Upgrade'";
        var count = session
            .CreateSQLQuery(sql)
            .UniqueResult<long>();

        return count > 0;
    }
}

public interface IUpgradeService
{
    void ExecuteUpgrades(DatabaseConnectionKeys databaseKey);
    void ExecuteMainUpgrades();
}