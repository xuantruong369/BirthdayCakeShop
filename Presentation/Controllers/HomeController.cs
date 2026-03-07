using System.Diagnostics;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _service;

    public HomeController(ILogger<HomeController> logger, IProductService service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Index()
    {
        var product = _service.GetProducts();
        return Ok(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
