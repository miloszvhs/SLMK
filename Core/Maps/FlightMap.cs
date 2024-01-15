using Core.Entities;
using FluentNHibernate.Mapping;

namespace Core.Maps;

public class FlightMap : ClassMap<Flight>
{
    public FlightMap()
    {
        Table("Flights");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Departure);
        Map(x => x.Destination);
        Map(x => x.FlightDate);
        Map(x => x.Price);
        Map(x => x.AvailableSeats);
        HasMany(x => x.Reservations)
            .Cascade.All()
            .Not.LazyLoad()
            .Inverse()
            .KeyColumn("FlightId");
    }
}