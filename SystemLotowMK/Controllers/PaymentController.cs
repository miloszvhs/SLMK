using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemLotowMK.Application.Services;
using SystemLotowMK.Domain.Entities;

namespace SystemLotowMK.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly IPaymentService _paymentService;
    
    public PaymentController(SignInManager<User> signInManager, IPaymentService paymentService)
    {
        _signInManager = signInManager;
        _paymentService = paymentService;
    }

    [HttpGet]
    public IActionResult Pay(int reservationId)
    {
        var user = _signInManager.UserManager.GetUserAsync(User).Result;
        _paymentService.PayForReservation(reservationId, user);

        return RedirectToAction("Index", "Reservation");
    }
}