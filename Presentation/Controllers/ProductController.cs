using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetAllProducts()
        {
            var productList = await _service.GetProducts();

            var productListView = productList.Select(p => new ProductListViewModel
            {
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToList();
            return View(productListView);
        }
    }
}