using Microsoft.AspNetCore.Mvc;
using SystemLotowMK.Application.Services;
using SystemLotowMK.Domain.Interfaces.Infrastructure;

namespace SystemLotowMK.Controllers;

public class FlightController : Controller
{
    private readonly IFlightService _flightService;
    private readonly IReservationRepository _reservationRepository;
    
    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public IActionResult Index()
    {
        var flights = _flightService.GetAllFlights();
        
        return View(flights);
    }
    
    [HttpGet]
    public IActionResult Book(int flightId)
    {
        var flight = _flightService.GetFlight(flightId);
        if(flight == null)
            return RedirectToAction("Index");
        
        return View(flight);
    }
}