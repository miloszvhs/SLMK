using SystemLotowMK.Domain.Entities.Common;

namespace SystemLotowMK.Domain.Entities;

public class Seat : BaseEntity
{
    public string SeatNumber { get; set; }
    public int FlightId { get; set; }
    public Flight Flight { get; set; }
    public Reservation Reservation { get; set; }
}