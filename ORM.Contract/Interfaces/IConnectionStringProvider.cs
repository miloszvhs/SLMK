using ORM.Contract.Enums;

namespace ORM.Contract.Interfaces;

public interface IConnectionStringProvider
{
    string GetConnectionString(string databaseKey);
    Dictionary<DatabaseConnectionKeys, string> GetAllConnectionStrings();
    void SaveConnectionString(string databaseKey, string connectionString);
}