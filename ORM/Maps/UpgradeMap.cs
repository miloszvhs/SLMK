using FluentNHibernate.Mapping;
using ORM.Entities;

namespace ORM.Maps;

public class UpgradeMap : ClassMap<Upgrade>
{
    public UpgradeMap()
    {
        Table("Upgrade");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Number).Not.Nullable();
        Map(x => x.ConnectionKey).Not.Nullable();
    }
}