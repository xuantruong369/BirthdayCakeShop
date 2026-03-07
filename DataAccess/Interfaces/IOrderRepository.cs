using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Order GetById(string id);

        void Add(Order order);

        void Update(Order order);

        void Delete(string id);

        IEnumerable<Order> GetByCustomer(string customerId);
    }
}