using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByUserId(int userId);
    }
}