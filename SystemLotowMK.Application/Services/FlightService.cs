using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Domain.Interfaces.Infrastructure;

namespace SystemLotowMK.Application.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;

    public FlightService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public List<Flight> GetAllFlights()
    {
        return _flightRepository.GetFlights();
        
    }

    public void CreateReservationForUser(int flightId, string userId)
    {
        
    }
}

public interface IFlightService
{
    public List<Flight> GetAllFlights();
    void CreateReservationForUser(int flightId, string userId);
}