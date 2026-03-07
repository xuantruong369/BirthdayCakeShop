using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetByOrder(int orderId);
    }
}