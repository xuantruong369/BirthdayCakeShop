using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetByCustomer(int customerId);
    }
}