using SystemLotowMK.Domain.Entities.Common;

namespace SystemLotowMK.Domain.Entities;

public class Reservation : BaseEntity
{
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public Payment Payment { get; set; }
}