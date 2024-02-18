using SystemLotowMK.Domain.Entities.Common;

namespace SystemLotowMK.Domain.Entities;

public class Seat : BaseEntity
{
    public  string Number { get; set; }
    public int FlightId { get; set; }
    public virtual Flight Flight { get; set; }
    public virtual Reservation Reservation { get; set; }
}