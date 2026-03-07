using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<IEnumerable<ProductImage>> GetByProduct(int productId)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }
    }
}