using NHibernate.AspNetCore.Identity;

namespace Core.Entities;

public class Reservation
{
    public virtual int Id { get; set; }
    public virtual string UserId { get; set; }
    public virtual Flight Flight { get; set; }
    public virtual DateTime ReservationDate { get; set; }
    public virtual string Status { get; set; }
}