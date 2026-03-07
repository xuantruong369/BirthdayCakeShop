using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetAll();

        IEnumerable<OrderDetail> GetByOrder(string orderId);

        void Add(OrderDetail orderDetail);

        void Update(OrderDetail orderDetail);

        void Delete(int id);
    }
}