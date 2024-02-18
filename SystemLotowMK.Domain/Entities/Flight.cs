using SystemLotowMK.Domain.Entities.Common;

namespace SystemLotowMK.Domain.Entities;

public class Flight : BaseEntity
{
    public string FlightNumber { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public List<Seat> Seats { get; set; }
    public decimal Price { get; set; }
}