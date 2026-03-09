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

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> ProductList()
    {
        var productList = await _service.GetAllProducts();

        var productListView = productList.Select(p => new ProductListViewModel
        {
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl
        });
        return Ok(productListView);
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
