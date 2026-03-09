using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _context;
        public ProductService(IProductRepository context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductListDTO>> GetProducts()
        {
            var products = await _context.GetAllProducts();

            return products.Select(p => new ProductListDTO
            {
                Name = p.ProductName,
                Price = p.ProductDetails.Min(d => d.Price),
                ImageUrl = p.Thumbnail
            });
        }
    }
}