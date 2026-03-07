using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BirthdayCakeShopDbContext _context;

        public OrderRepository(BirthdayCakeShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(string id)
        {
            return _context.Orders.Find(id);
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var order = _context.Orders.Find(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Order> GetByCustomer(string customerId)
        {
            return _context.Orders
                .Where(o => o.CustomerId == customerId)
                .ToList();
        }
    }
}