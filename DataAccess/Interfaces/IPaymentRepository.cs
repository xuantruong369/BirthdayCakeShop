using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetByOrder(int orderId);
    }
}