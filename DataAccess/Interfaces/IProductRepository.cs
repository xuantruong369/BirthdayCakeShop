using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(int categoryId);
    }
}