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

    public void CancelReservation(int id, string userId)
    {
        _reservationRepository.DeleteReservation(id, userId);
    }

    public List<Reservation> GetReservationsByUser(string userId)
    { 
        return _reservationRepository.GetReservationsByUser(userId);
    }

    public void CreateReservationForUser(Flight flight, User? user)
    {
        var seat = flight.Seats.FirstOrDefault(x => x.Reservation == null); 
        var reservation = new Reservation
        {
            Seat = seat,
            User = user,
        };
        
        var payment = new Payment
        {
            Reservation = reservation,
            Amount = flight.Price,
            PaymentDate = DateTime.UtcNow,
            Status = PaymentStatus.Pending
        };
        
        reservation.Payment = payment;
        
        _reservationRepository.AddReservation(reservation);
    }

    public Reservation? GetReservation(int reservationId, string userId)
    {
        return _reservationRepository.GetReservationById(reservationId, userId);
    }
}

public interface IReservationService
{
    public void AddReservation(Reservation reservation);
    public void CancelReservation(int id, string userId);
    public List<Reservation> GetReservationsByUser(string userId);
    void CreateReservationForUser(Flight flightId, User? userId);
    Reservation? GetReservation(int reservationId, string userId);
}