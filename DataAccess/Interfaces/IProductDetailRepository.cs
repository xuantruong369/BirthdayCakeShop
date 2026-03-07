using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProductDetailRepository
    {
        IEnumerable<ProductDetail> GetAll();

        ProductDetail GetById(string id);

        IEnumerable<ProductDetail> GetByProduct(string productId);

        void Add(ProductDetail detail);

        void Update(ProductDetail detail);

        void Delete(string id);
    }
}