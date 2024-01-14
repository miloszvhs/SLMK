using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace ORM.Services;

public interface IConnectionService
{
    public void Commit(string moduleName, Action<ISession> action);
}

public class ConnectionService : IConnectionService
{
    private readonly ISessionFactoryService _sessionFactoryService;

    public ConnectionService(ISessionFactoryService sessionFactoryService)
    {
        _sessionFactoryService = sessionFactoryService;
    }

    public void Commit(string moduleName, Action<ISession> action)
    {
        using var session = _sessionFactoryService
            .GetSessionFactory(moduleName)
            .OpenSession();
        using var transaction = session.BeginTransaction();
        
        try
        {
            action(session);
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }

        try
        {
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
    }
}
