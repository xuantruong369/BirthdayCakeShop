using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly BirthdayCakeShopDbContext _context;

        public ProductDetailRepository(BirthdayCakeShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDetail> GetAll()
        {
            return _context.ProductDetails.ToList();
        }

        public ProductDetail GetById(string id)
        {
            return _context.ProductDetails.Find(id);
        }

        public IEnumerable<ProductDetail> GetByProduct(string productId)
        {
            return _context.ProductDetails
                .Where(pd => pd.ProductId == productId)
                .ToList();
        }

        public void Add(ProductDetail detail)
        {
            _context.ProductDetails.Add(detail);
            _context.SaveChanges();
        }

        public void Update(ProductDetail detail)
        {
            _context.ProductDetails.Update(detail);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var detail = _context.ProductDetails.Find(id);

            if (detail != null)
            {
                _context.ProductDetails.Remove(detail);
                _context.SaveChanges();
            }
        }
    }
}