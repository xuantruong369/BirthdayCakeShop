using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly BirthdayCakeShopDbContext _context;

        public ProductImageRepository(BirthdayCakeShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductImage> GetAll()
        {
            return _context.ProductImages.ToList();
        }

        public IEnumerable<ProductImage> GetByProduct(string productId)
        {
            return _context.ProductImages
                .Where(i => i.ProductId == productId)
                .ToList();
        }

        public void Add(ProductImage image)
        {
            _context.ProductImages.Add(image);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var image = _context.ProductImages.Find(id);

            if (image != null)
            {
                _context.ProductImages.Remove(image);
                _context.SaveChanges();
            }
        }
    }
}