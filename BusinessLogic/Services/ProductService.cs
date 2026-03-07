using BusinessLogic.Interfaces;
using DataAccess.Entities;
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

        public IEnumerable<Product> GetProducts()
        {
            return _repo.GetAll();
        }

        public Product GetProduct(string id)
        {
            return _repo.GetById(id);
        }
    }
}