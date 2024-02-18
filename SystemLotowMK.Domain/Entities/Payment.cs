using SystemLotowMK.Domain.Entities.Common;

namespace SystemLotowMK.Domain.Entities;

public class Payment : BaseEntity
{
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int ReservationId { get; set; }
    public virtual Reservation Reservation { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Cancelled
}
