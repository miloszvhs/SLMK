using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using ORM.Contract.Interfaces;

namespace ORM.Services;

public class SessionFactoryService : ISessionFactoryService
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public SessionFactoryService(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public ISessionFactory GetSessionFactory(string moduleName)
    {
        var sessionFactory = Fluently
            .Configure()
            .Database(
                PostgreSQLConfiguration.PostgreSQL83
                    .ConnectionString(_connectionStringProvider.GetConnectionString(moduleName))
            )
            .Mappings(m =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.GetTypes().Any(y => typeof(IMappingFromAssembly).IsAssignableFrom(y)));
                
                foreach (var assembly in assemblies)
                    m.FluentMappings.AddFromAssembly(assembly);
            })
            .BuildSessionFactory();
        
        return sessionFactory;
    }
}

public interface ISessionFactoryService
{
    ISessionFactory GetSessionFactory(string moduleName);
}