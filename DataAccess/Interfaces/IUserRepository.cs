using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User GetByUsername(string username);

        void Add(User user);

        void Update(User user);

        void Delete(string username);
    }
}