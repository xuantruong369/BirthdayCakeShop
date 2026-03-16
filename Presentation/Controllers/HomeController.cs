using System.Diagnostics;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.ViewModels;

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

    // public async Task<IActionResult> Index()
    // {
    //     var product = await _service.GetAllProducts();

    //     // if (!ModelState.IsValid)
    //     // {
    //     //     return Ok("False");
    //     // }

    //     var productViewModel = product.Select(p => new ProductViewModel
    //     {
    //         Name = p.Name
    //     }).ToList();
    //     return Ok(productViewModel);
    // }

    public async Task<IActionResult> Index()
    {
        var productList = await _service.GetProducts();

        var productListView = productList.Select(p => new ProductListViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl
        }).ToList();
        return View(productListView);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
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
