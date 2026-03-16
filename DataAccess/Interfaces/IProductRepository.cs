using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategory(int categoryId);
        //
        Task<IEnumerable<Product>> GetAllProducts();
        //
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetAllAdminProducts();
        Task DeleteProduct(int productId);
    }
}