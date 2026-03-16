using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<Customer?> GetByUserId(int? userId)
        {
            return await _context.Customers
                .Include(p => p.Cart)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Customer?> GetCustomerById(int? Id)
        {
            return await _context.Customers
                .FindAsync(Id);
                
        }
    }
}