using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _repo.GetAll();
            return products.Select(p => new ProductDTO
            {
                Id = p.ProductId,
                Name = p.ProductName
            });
        }
    }
}