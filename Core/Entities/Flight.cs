using NHibernate.AspNetCore.Identity;

namespace Core.Entities;

public class Flight
{
    public virtual int Id { get; set; }
    public virtual string Departure { get; set; }
    public virtual string Destination { get; set; }
    public virtual DateTime FlightDate { get; set; }
    public virtual decimal Price { get; set; }
    public virtual int AvailableSeats { get; set; }
    public virtual IList<Reservation> Reservations { get; set; }
}