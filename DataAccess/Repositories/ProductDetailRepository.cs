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

        public async Task<ProductDetail?> GetHeadProductDetailByProductId(int productId)
        {
            return await _context.ProductDetails
                .Where(x => x.ProductId == productId)
                .FirstOrDefaultAsync();
        }

        public async Task<ProductDetail> GetProductDetailById(int? id)
        {
            return await _context.ProductDetails
                .Include(p => p.Product) // Load dữ liệu bảng liên quan
                .FirstOrDefaultAsync(x => x.ProductDetailId == id); // Tìm theo Id
        }
    }
}