using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Domain.Interfaces.Infrastructure;

namespace SystemLotowMK.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public IEnumerable<Payment> GetPaymentsForUser(User? user)
    {
        var payments = _paymentRepository.GetPaymentsForUser(user);
        return payments;
    }

    public void PayForReservation(int id, User user)
    {
        var payment = _paymentRepository
            .GetPaymentsForUser(user)
            .FirstOrDefault(x => x.ReservationId == id);
        
        payment.Status = PaymentStatus.Paid;
        _paymentRepository.Update(payment);
    }

    public IEnumerable<Payment> GetAllPayments()
    {
        return _paymentRepository.GetAllPayments();
    }

    public void SavePayment(Payment payment)
    {
        _paymentRepository.Update(payment);
    }
}

public interface IPaymentService
{
    public IEnumerable<Payment> GetPaymentsForUser(User? user);
    void PayForReservation(int id, User user);
    IEnumerable<Payment> GetAllPayments();
    void SavePayment(Payment payment);
}