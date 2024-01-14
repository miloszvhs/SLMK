using NHibernate;
using ORM.Contract.Enums;
using ORM.Contract.Interfaces;

namespace ORM.Upgrades;

public class Upgrade1 : ISqlExecutor
{
    public int Number => 1;
    public DatabaseConnectionKeys ConnectionKey => DatabaseConnectionKeys.Orm;
    public void Execute(ISession session)
    {
        CreateUpgradeTable(session);
    }
    
    private void CreateUpgradeTable(ISession session)
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
}