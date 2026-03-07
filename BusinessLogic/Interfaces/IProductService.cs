using DataAccess.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IProductService
{
    IEnumerable<Product> GetProducts();
    Product GetProduct(string id);
}
}