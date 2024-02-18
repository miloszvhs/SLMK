using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Domain.Interfaces.Infrastructure;

namespace SystemLotowMK.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public void AddReservation(Reservation reservation)
    {
        
    }

    public void RemoveReservation(int id, string userId)
    {
        
    }

    public List<Reservation> GetReservationsByUser(string userId)
    { 
        return _reservationRepository.GetReservationsByUser(userId);
    }
}

public interface IReservationService
{
    public void AddReservation(Reservation reservation);
    public void RemoveReservation(int id, string userId);
    public List<Reservation> GetReservationsByUser(string userId);
}