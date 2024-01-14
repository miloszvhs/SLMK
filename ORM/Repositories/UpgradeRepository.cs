using NHibernate;
using ORM.Entities;

namespace ORM.Repositories;

public class UpgradeRepository : IUpgradeRepository
{
    public void Save(ISession session, Upgrade upgrade)
    {
        session.SaveOrUpdate(upgrade);
    }
}

public interface IUpgradeRepository
{
    void Save(ISession session, Upgrade upgrade);
}