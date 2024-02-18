using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemLotowMK.Application.Services;
using SystemLotowMK.Domain.Entities;

namespace SystemLotowMK.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IFlightService _flightService;
    private readonly SignInManager<User> _signInManager;

    
    public ReservationController(SignInManager<User> signInManager,
        IReservationService reservationService,
        IFlightService flightService)
    {
        _signInManager = signInManager;
        _reservationService = reservationService;
        _flightService = flightService;
    }

    public IActionResult Index()
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        var reservations = _reservationService.GetReservationsByUser(user.Id);
        
        return View(reservations);
    }
    
    [HttpGet]
    public IActionResult Cancel(int reservationId)
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        _reservationService.CancelReservation(reservationId, user.Id);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Reserve(int flightId)
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        var flight = _flightService.GetFlight(flightId);
        if(!_flightService.CheckSeatsAvailability(flight.Id))
            return RedirectToAction("Index");
        
        _reservationService.CreateReservationForUser(flight, user);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Reservation(int reservationId, string userId)
    {
        var reservation = _reservationService.GetReservation(reservationId, userId);
        if (reservation == null)
            return RedirectToAction("Index");
        
        return View(reservation);
    }
}