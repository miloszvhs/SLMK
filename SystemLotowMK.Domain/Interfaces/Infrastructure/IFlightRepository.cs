using SystemLotowMK.Domain.Entities;

namespace SystemLotowMK.Domain.Interfaces.Infrastructure;

public interface IFlightRepository
{
    Flight GetFlight(int id);
    List<Flight> GetFlights();
    void AddFlight(Flight flight);
    void UpdateFlight(Flight flight);
    void DeleteFlight(int id);
}