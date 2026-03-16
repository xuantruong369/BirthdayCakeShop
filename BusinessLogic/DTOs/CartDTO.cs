using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public class CartDTO
    {
        public int CartId {get; set;}
        public List<CartItemDTO> CartItems = new List<CartItemDTO>();
    }
}