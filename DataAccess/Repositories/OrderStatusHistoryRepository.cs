using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderStatusHistoryRepository : Repository<OrderStatusHistory>, IOrderStatusHistoryRepository
    {
        public OrderStatusHistoryRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<IEnumerable<OrderStatusHistory>> GetByOrder(int orderId)
        {
            return await _context.OrderStatusHistories
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }
    }
}