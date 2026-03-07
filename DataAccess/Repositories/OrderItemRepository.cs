using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<IEnumerable<OrderItem>> GetByOrder(int orderId)
        {
            return await _context.OrderItems
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }
    }
}