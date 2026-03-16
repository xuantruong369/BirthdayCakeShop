using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Interfaces
{
    public interface ICartService
    {
        Task<CartDTO> GetOrCreateCart(int? userId);
        Task AddCartItem(int? userId, AddCartItemDTO addCartItemDTO);
        Task DeleteCartItem(int cartItemId);
    }
}