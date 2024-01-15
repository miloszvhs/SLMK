using Core.Entities;
using Core.Enums;
using NHibernate;
using NHibernate.AspNetCore.Identity;

namespace Core.Repositories;

public interface IReservationRepository
{
    public List<Reservation> GetAllReservationByUserId(ISession session, string userId);
    public Reservation GetReservationById(ISession session, string userId, int id);
    public void CreateReservation(ISession session, int flightId, string userId);
}

public class ReservationRepository : IReservationRepository
{
    public List<Reservation> GetAllReservationByUserId(ISession session, string userId)
    {
        var result = session.QueryOver<Reservation>()
            .Where(x => x.UserId == userId)
            .List();
        
        return result.ToList();
    }

    public Reservation GetReservationById(ISession session, string userId, int id)
    {
        var result = session.QueryOver<Reservation>()
            .Where(x => x.UserId == userId)
            .And(x => x.Id == id)
            .SingleOrDefault();
        
        return result;
    }

    public void CreateReservation(ISession session, int flightId, string userId)
    {
        var flight = session.Get<Flight>(flightId);
        var reservation = new Reservation
        {
            Flight = flight,
            UserId = userId,
            Status = ReservationStatusEnum.Active.ToString(), 
            ReservationDate = DateTime.Now
        };

        session.Save(reservation);
    }
}

