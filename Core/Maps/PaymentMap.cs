using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Maps;

public class PaymentMap : ClassMap<Payment>
{
    public PaymentMap()
    {
        Table("Payments");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Amount).Not.Nullable();
        Map(x => x.PaymentDate).Not.Nullable();
        Map(x => x.PaymentMethod).Not.Nullable();
        References(x => x.Reservation)
            .Cascade.All()
            .Not.LazyLoad();
    }
}