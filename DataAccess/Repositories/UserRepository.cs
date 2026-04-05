using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<User?> GetByUserId(int? userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}