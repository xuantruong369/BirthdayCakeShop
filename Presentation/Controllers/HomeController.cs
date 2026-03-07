using System.Diagnostics;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _product;

    public HomeController(ILogger<HomeController> logger, IProductRepository product)
    {
        _logger = logger;
        _product = product;
    }

    public async Task<IActionResult> Index()
    {
        var product = await _product.GetAll();
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
