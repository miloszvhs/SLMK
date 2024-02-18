using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Domain.Interfaces.Infrastructure;
using SystemLotowMK.Infrastructure.ApplicationContexts;

namespace SystemLotowMK.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FlightRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Flight GetFlight(int id) => _dbContext.Flights.SingleOrDefault(x => x.Id == id);

    public List<Flight> GetFlights() => _dbContext.Flights.ToList();

    public void AddFlight(Flight flight)
    {
        _dbContext.Flights.Add(flight);
        _dbContext.SaveChanges();
    }

    public void UpdateFlight(Flight flight)
    {
        _dbContext.Flights.Update(flight);
        _dbContext.SaveChanges();
    }

    public void DeleteFlight(int id)
    {
        var flight = _dbContext.Flights.SingleOrDefault(x => x.Id == id);
        if (flight == null) 
            return;
        
        _dbContext.Flights.Remove(flight);
        _dbContext.SaveChanges();
    }
}