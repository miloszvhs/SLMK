using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Domain.Interfaces.Infrastructure;
using SystemLotowMK.Infrastructure.ApplicationContexts;

namespace SystemLotowMK.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Reservation> GetReservationsByUser(string userId) 
        => _dbContext.Reservations.Where(x => x.UserId == userId).ToList();
                

    public Reservation GetReservationById(int id, string userId) 
        => _dbContext.Reservations.SingleOrDefault(x => x.Id == id && x.UserId == userId);

    public void AddReservation(Reservation reservation)
    {
        _dbContext.Reservations.Add(reservation);
        _dbContext.SaveChanges();
    }

    public void UpdateReservation(Reservation reservation)
    {
        _dbContext.Reservations.Update(reservation);
        _dbContext.SaveChanges();
    }

    public void DeleteReservation(int id, string userId)
    {
        var reservation = _dbContext.Reservations.SingleOrDefault(x => x.Id == id && x.UserId == userId);
        if (reservation == null) 
            return;
        
        _dbContext.Reservations.Remove(reservation);
        _dbContext.SaveChanges();
    }
}

