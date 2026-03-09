using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(BirthdayCakeShopDbContext context) : base(context) { }

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
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}