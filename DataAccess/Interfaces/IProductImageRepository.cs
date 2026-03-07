using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task<IEnumerable<ProductImage>> GetByProduct(int productId);
    }
}