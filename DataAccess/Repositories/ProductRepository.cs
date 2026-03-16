using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return;
            }

            var productDetails = await _context.ProductDetails
                .Where(pd => pd.ProductId == productId)
                .ToListAsync();

            var productImages = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();

            _context.ProductDetails.RemoveRange(productDetails);
            _context.ProductImages.RemoveRange(productImages);
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAdminProducts()
        {
            return await _context.Products
                .Include(p => p.ProductDetails)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products
                .Include(p => p.ProductDetails)
                .Include(p => p.ProductImages)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(int categoryId)
        {
            return await _context.Products
                .Include(p => p.ProductDetails)
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductDetails)
                .Include(p => p.ProductImages)
                .FirstAsync(p => p.ProductId == id);
        }
    }
}