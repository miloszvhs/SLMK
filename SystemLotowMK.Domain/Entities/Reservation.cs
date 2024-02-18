using SystemLotowMK.Domain.Entities.Common;

namespace SystemLotowMK.Domain.Entities;

public class Reservation : BaseEntity
{
    public int SeatId { get; set; }
    public virtual Seat Seat { get; set; }
    public string UserId { get; set; }
    public virtual User User { get; set; }
    public virtual Payment Payment { get; set; }
}