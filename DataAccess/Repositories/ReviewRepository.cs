using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Review>> GetByProduct(int productId)
        {
            return await _context.Reviews
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }
    }
}