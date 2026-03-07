using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IProductRepository
{
    IEnumerable<Product> GetAll();

    Product GetById(string id);

    void Add(Product product);

    void Update(Product product);

    void Delete(string id);

    IEnumerable<Product> Search(string keyword);
} 
}