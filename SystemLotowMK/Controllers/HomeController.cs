using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemLotowMK.Models;

namespace SystemLotowMK.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }

    [Authorize]
    public IActionResult Flights()
    {
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [Authorize]
    public IActionResult Reservations()
    {
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}