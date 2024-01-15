namespace Core.Entities;

public class Payment
{
    public virtual int Id { get; set; }
    public virtual Reservation Reservation { get; set; }
    public virtual decimal Amount { get; set; }
    public virtual DateTime PaymentDate { get; set; }
    public virtual string PaymentMethod { get; set; }
}