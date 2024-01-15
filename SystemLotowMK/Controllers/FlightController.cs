using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ORM.Contract.Enums;
using ORM.Services;
using IdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace SystemLotowMK.Controllers;

public class FlightController : Controller
{
    private readonly SignInManager<NHibernate.AspNetCore.Identity.IdentityUser> _signInManager;
    private readonly IFlightRepository _flightRepository;
    private readonly IConnectionService _connectionService;
    private readonly IReservationRepository _reservationRepository;
    
    public FlightController(SignInManager<IdentityUser> signInManager,
        IFlightRepository flightRepository, IConnectionService connectionService, 
        IReservationRepository reservationRepository)
    {
        _signInManager = signInManager;
        _flightRepository = flightRepository;
        _connectionService = connectionService;
        _reservationRepository = reservationRepository;
    }

    public IActionResult Index()
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;

        List<Flight> flights = new List<Flight>();
        _connectionService.Commit(DatabaseConnectionKeys.Core.ToString(), session =>
        {
            flights = _flightRepository.GetAllFlights(session);
        });
        
        return View(flights);
    }
    
    [HttpGet]
    public IActionResult Book(int flightId)
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        
        _connectionService.Commit(DatabaseConnectionKeys.Core.ToString(), session =>
        {
            _reservationRepository.CreateReservation(session, flightId, user.Id);
        });
        
        return RedirectToAction("Index");
    }
}