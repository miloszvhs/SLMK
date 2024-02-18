using SystemLotowMK.Application.Interfaces;
using SystemLotowMK.Application.Services;
using SystemLotowMK.Domain.Entities;

namespace SystemLotowMK.Application.Jobs;

public class PaymentObserver : IJob
{
    private readonly IPaymentService _paymentService;

    public PaymentObserver(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public void Observe() 
    {
        var payments = _paymentService.GetAllPayments();

        foreach (var payment in payments)
        {
            if (payment.Status == PaymentStatus.Pending)
            {
                var time = payment.PaymentDate;
                if (time.AddMinutes(10) < DateTime.UtcNow)
                {
                    payment.Status = PaymentStatus.Cancelled;
                    _paymentService.SavePayment(payment);                    
                }
            }
        }
    }
}

