using Azure.Core;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(p => p.Customer)
                .Include(p => p.OrderItems)
                    .ThenInclude(oi => oi.ProductDetail)
                        .ThenInclude(pd => pd.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomer(int customerId)
        {
            return await _context.Orders
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }


    }
}