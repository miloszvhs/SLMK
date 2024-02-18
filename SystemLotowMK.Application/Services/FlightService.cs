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
    
    public bool CheckSeatsAvailability(int flightId)
    {
        var flight = _flightRepository.GetFlight(flightId);
        var seats = flight.Seats.Where(x => x.Reservation == null).ToList();
        
        return seats.Any();
    }

    public Flight? GetFlight(int flightId) => _flightRepository.GetFlight(flightId);
}

public interface IFlightService
{
    public List<Flight> GetAllFlights();
    void CreateReservationForUser(int flightId, string userId);
    bool CheckSeatsAvailability(int flightId);
    Flight? GetFlight(int flightId);
}