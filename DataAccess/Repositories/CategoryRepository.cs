using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoryRepository : Repository<CakeCategory>, ICategoryRepository
    {
        public CategoryRepository(BirthdayCakeShopDbContext context) : base(context) { }
        public async Task<IEnumerable<CakeCategory>> GetAllCategorys()
        {
            return await _context.CakeCategories
                .Include(p => p.Products)
                    .ThenInclude(p => p.ProductDetails)
                        .ThenInclude(p => p.OrderItems)
                .ToListAsync();
        }
    }
}