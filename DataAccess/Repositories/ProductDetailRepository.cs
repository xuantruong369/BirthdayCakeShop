using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductDetailRepository : Repository<ProductDetail>, IProductDetailRepository
    {
        public ProductDetailRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<IEnumerable<ProductDetail>> GetByProduct(int productId)
        {
            return await _context.ProductDetails
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }
    }
}