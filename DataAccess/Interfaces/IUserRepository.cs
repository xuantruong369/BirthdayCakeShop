using DataAccess.Entities;

namespace DataAccess.Interfaces 
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsername(string username);
    }
}