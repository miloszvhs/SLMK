using Microsoft.Extensions.DependencyInjection;
using ORM.Contract.Enums;
using ORM.Contract.Interfaces;

namespace ORM;

public static class AddConnectionStringsExtension
{
    public static void AddConnectionStrings(this IServiceProvider serviceProvider)
    {
        var connectionStringProvider = serviceProvider.GetService<IConnectionStringProvider>();
        if(connectionStringProvider == null)
            throw new Exception("Connection string provider is null");
        
        connectionStringProvider.SaveConnectionString(DatabaseConnectionKeys.Main.ToString(), "Server=localhost;Port=5432;Database=slmk_main;User Id=SLMK;Password=Test12345;");
        connectionStringProvider.SaveConnectionString(DatabaseConnectionKeys.Core.ToString(), "Server=localhost;Port=5432;Database=slmk_core;User Id=SLMK;Password=Test12345;");
    }
}