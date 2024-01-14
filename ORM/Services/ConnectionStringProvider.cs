using System.Collections.Concurrent;
using ORM.Contract.Enums;
using ORM.Contract.Interfaces;

namespace ORM.Services;

public class ConnectionStringProvider : IConnectionStringProvider 
{
    private ConcurrentDictionary<string, string> _connectionStrings = new();
    
    public string GetConnectionString(string databaseKey)
    {
        return _connectionStrings.TryGetValue(databaseKey, out var connectionString) 
            ? connectionString 
            : throw new Exception($"Connection string for database key {databaseKey} not found");
    }

    public Dictionary<DatabaseConnectionKeys, string> GetAllConnectionStrings()
    {
        return _connectionStrings.ToDictionary(x => Enum.Parse<DatabaseConnectionKeys>(x.Key), x => x.Value);
    }

    public void SaveConnectionString(string databaseKey, string connectionString)
    {
        _connectionStrings.AddOrUpdate(databaseKey, connectionString, (_, _) => connectionString);
    }
}