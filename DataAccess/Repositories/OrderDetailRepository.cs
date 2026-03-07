using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly BirthdayCakeShopDbContext _context;

        public OrderDetailRepository(BirthdayCakeShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public IEnumerable<OrderDetail> GetByOrder(string orderId)
        {
            return _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToList();
        }

        public void Add(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var detail = _context.OrderDetails.Find(id);

            if (detail != null)
            {
                _context.OrderDetails.Remove(detail);
                _context.SaveChanges();
            }
        }
    }
}