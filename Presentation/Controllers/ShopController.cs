using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _service;
        public ShopController(IProductService service)
        {
            _service = service;
        }
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
    }
}