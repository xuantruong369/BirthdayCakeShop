using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<Payment?> GetByOrder(int orderId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }
    }
}