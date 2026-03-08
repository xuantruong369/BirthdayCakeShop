using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetByCustomerId(int customerId);
    }
}