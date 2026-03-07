using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BirthdayCakeShopDbContext _context;

        public ProductRepository(BirthdayCakeShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(string id)
        {
            return _context.Products.Find(id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> Search(string keyword)
        {
            return _context.Products
                .Where(p => p.ProductName.Contains(keyword))
                .ToList();
        }
    }
}