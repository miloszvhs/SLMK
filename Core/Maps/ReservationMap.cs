using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Maps;

public class ReservationMap : ClassMap<Reservation>
{
    public ReservationMap()
    {
        Table("Reservations");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.ReservationDate).Not.Nullable();
        Map(x => x.Status).Not.Nullable();
        Map(x => x.UserId).Not.Nullable();
        References(x => x.Flight)
            .Cascade.All()
            .Not.LazyLoad()
            .Column("FlightId");
    }
}