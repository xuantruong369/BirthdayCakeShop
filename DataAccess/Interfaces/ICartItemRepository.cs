using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetByCart(int cartId);
    }
}