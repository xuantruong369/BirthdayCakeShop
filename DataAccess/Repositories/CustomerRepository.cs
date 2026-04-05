using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<Customer?> GetByUserId(int? userId)
        {
            return await _context.Customers
                .Include(p => p.Cart)
                .Include(p => p.User)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Customer?> GetCustomerById(int? Id)
        {
            return await _context.Customers
                .FindAsync(Id);

        }
    }
}