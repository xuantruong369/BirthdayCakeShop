using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProductDetailRepository : IRepository<ProductDetail>
    {
        Task<IEnumerable<ProductDetail>> GetByProduct(int productId);
        Task<ProductDetail> GetProductDetailById(int? id);
        Task<ProductDetail?> GetHeadProductDetailByProductId(int productId);
    }
}