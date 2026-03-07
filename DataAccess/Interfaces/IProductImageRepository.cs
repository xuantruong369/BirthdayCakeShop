using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProductImageRepository
    {
        IEnumerable<ProductImage> GetAll();

        IEnumerable<ProductImage> GetByProduct(string productId);

        void Add(ProductImage image);

        void Delete(int id);
    }
}