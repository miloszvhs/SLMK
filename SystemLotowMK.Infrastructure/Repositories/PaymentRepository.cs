using Microsoft.EntityFrameworkCore;
using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Domain.Interfaces.Infrastructure;
using SystemLotowMK.Infrastructure.ApplicationContexts;

namespace SystemLotowMK.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Payment> GetPaymentsForUser(User? user)
    {
        return _context.Payments.Where(p => user != null && p.Reservation.UserId == user.Id);
    }

    public void Update(Payment payment)
    {
        _context.Update(payment);
        _context.SaveChanges();
    }

    public IEnumerable<Payment> GetAllPayments()
    {
        return _context.Payments.Include(x => x.Reservation);
    }
}