using SystemLotowMK.Domain.Entities;

namespace SystemLotowMK.Domain.Interfaces.Infrastructure;

public interface IPaymentRepository
{
    IEnumerable<Payment> GetPaymentsForUser(User? user);
    void Update(Payment payment);
    IEnumerable<Payment> GetAllPayments();
}