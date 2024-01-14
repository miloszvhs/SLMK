using System.Reflection;
using Microsoft.Data.SqlClient;
using NHibernate;
using Npgsql;
using ORM.Contract.Enums;
using ORM.Contract.Interfaces;

namespace Core.Upgrades;

public class Upgrade1 : ISqlExecutor
{
    public int Number => 1;

    public DatabaseConnectionKeys ConnectionKey => DatabaseConnectionKeys.Core;

    public void Execute(ISession session)
    {
        ExecuteSqlFromFile(session, "upgrade1_identity.sql");
        ExecuteSqlFromFile(session, "upgrade2_users.sql");
        ExecuteSqlFromFile(session, "upgrade3_roles.sql");
    }
    
    private void ExecuteSqlFromFile(ISession session, string fileName)
    {
        var bin = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = $@"{bin}/Resources/{fileName}";
        var sql = File.ReadAllText(filePath);

        var connection = session.Connection as NpgsqlConnection;
        var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}