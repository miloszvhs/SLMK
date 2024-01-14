using NHibernate;
using ORM.Contract.Enums;

namespace ORM.Contract.Interfaces;

public interface ISqlExecutor
{
    int Number { get; }
    DatabaseConnectionKeys ConnectionKey { get; }
    void Execute(ISession session);    
}