using Core.Entities;
using NHibernate;

namespace Core.Repositories;

public interface IFlightRepository
{
    public List<Flight> GetAllFlights(ISession session);
}

public class FlightRepository : IFlightRepository
{
    public List<Flight> GetAllFlights(ISession session)
    {
        var result = session.QueryOver<Flight>()
            .List();
        
        return result.ToList();
    }
}