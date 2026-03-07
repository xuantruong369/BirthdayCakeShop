using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IOrderStatusHistoryRepository : IRepository<OrderStatusHistory>
    {
        Task<IEnumerable<OrderStatusHistory>> GetByOrder(int orderId);
    }
}