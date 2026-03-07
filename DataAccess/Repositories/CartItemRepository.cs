using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<IEnumerable<CartItem>> GetByCart(int cartId)
        {
            return await _context.CartItems
                .Where(x => x.CartId == cartId)
                .ToListAsync();
        }
    }
}