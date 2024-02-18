using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemLotowMK.Application.Services;
using SystemLotowMK.Domain.Entities;
using SystemLotowMK.Domain.Interfaces.Infrastructure;

namespace SystemLotowMK.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly SignInManager<User> _signInManager;

    
    public ReservationController(SignInManager<User> signInManager,
        IReservationService reservationService)
    {
        _signInManager = signInManager;
        _reservationService = reservationService;
    }

    public IActionResult Index()
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        var reservations = _reservationService.GetReservationsByUser(user.Id);
        
        return View(reservations);
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        _reservationService.RemoveReservation(id, user.Id);
        
        return RedirectToAction("Index");
    }
}