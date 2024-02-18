using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemLotowMK.Application.Services;
using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Domain.Interfaces.Infrastructure;

namespace SystemLotowMK.Controllers;

public class FlightController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly IFlightService _flightService;
    private readonly IReservationRepository _reservationRepository;
    
    public FlightController(SignInManager<User> signInManager,
        IFlightService flightService)
    {
        _signInManager = signInManager;
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
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        _flightService.CreateReservationForUser(flightId, user.Id);
        
        return RedirectToAction("Index");
    }
}