using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModels;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductAPIController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{maTheLoai}")]
        public async Task<IEnumerable<ProductListViewModel>> GetProductsByColor(int maTheLoai)
        {
            var productList = await _productService.GetByCategoryId(maTheLoai);

            var productListView = productList.Select(p => new ProductListViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToList();
            return productListView;
        }
    }
}