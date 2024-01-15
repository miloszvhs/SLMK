using Core.Entities;
using Core.Enums;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ORM.Contract.Enums;
using ORM.Services;
using IdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace SystemLotowMK.Controllers;

[Authorize]
public class BookingController : Controller
{
    private readonly IReservationRepository _reservationRepository;
    private readonly SignInManager<NHibernate.AspNetCore.Identity.IdentityUser> _signInManager;
    private readonly IConnectionService _connectionService;

    
    public BookingController(IReservationRepository reservationRepository, 
        SignInManager<IdentityUser> signInManager,
        IConnectionService connectionService)
    {
        _reservationRepository = reservationRepository;
        _signInManager = signInManager;
        _connectionService = connectionService;
    }

    public IActionResult Index()
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        
        List<Reservation> reservations = new List<Reservation>();
        _connectionService.Commit(DatabaseConnectionKeys.Core.ToString(), session =>
        {
            reservations = _reservationRepository.GetAllReservationByUserId(session, user.Id);
        });
        
        return View(reservations);
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        
        _connectionService.Commit(DatabaseConnectionKeys.Core.ToString(), session =>
        {
            var reservation = _reservationRepository.GetReservationById(session, user.Id, id);
            session.Delete(reservation);
        });
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Cancel(int reservationid)
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        
        _connectionService.Commit(DatabaseConnectionKeys.Core.ToString(), session =>
        {
            var reservation = _reservationRepository.GetReservationById(session, user.Id, reservationid);
            reservation.Status = ReservationStatusEnum.Canceled.ToString();
            session.Update(reservation);
        });
        
        return RedirectToAction("Index");
    }
}