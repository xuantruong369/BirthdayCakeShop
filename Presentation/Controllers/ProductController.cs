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

        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var productList = await _service.GetByCategoryId(categoryId);

            var productListView = productList.Select(p => new ProductListViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToList();
            return View(productListView);
        }

        public async Task<IActionResult> GetProducts()
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

        public async Task<IActionResult> GetProductDetail(int maSP)
        {
            var detail = await _service.GetProductDetail(maSP);

            var detailView = new ProductDetailView()
            {
                ProductId = detail.ProductId,
                Id = detail.Id,
                Name = detail.Name,
                Description = detail.Description,
                Thumbnail = detail.Thumbnail,
                CategoryName = detail.CategoryName,
                ImageUrls = detail.ImageUrls,
                CakeSize = detail.CakeSize,
                Flavor = detail.Flavor,
                Price = detail.Price
            };
            return View(detailView);
        }
    }
}