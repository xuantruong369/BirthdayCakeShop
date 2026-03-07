using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProductDetailRepository : IRepository<ProductDetail>
    {
        Task<IEnumerable<ProductDetail>> GetByProduct(int productId);
    }
}