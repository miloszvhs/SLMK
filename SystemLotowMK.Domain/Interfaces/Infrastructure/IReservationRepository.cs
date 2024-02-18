using SystemLotowMK.Domain.Entities;

namespace SystemLotowMK.Domain.Interfaces.Infrastructure;

public interface IReservationRepository
{
    public List<Reservation> GetReservationsByUser(string userId);
    public Reservation? GetReservationById(int id, string userId);
    
    public void AddReservation(Reservation reservation);
    
    public void UpdateReservation(Reservation reservation);
    
    public void DeleteReservation(int id, string userId);
}