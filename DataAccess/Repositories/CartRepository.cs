using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<Cart> GetByCustomerId(int customerId)
        {
            return await _context.Carts
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }
    }
}