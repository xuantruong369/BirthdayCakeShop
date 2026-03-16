using DataAccess.Entities;

namespace DataAccess.Interfaces 
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByUsername(string username);
        Task<bool> UsernameExists(string username);
    }
}