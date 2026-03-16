using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(AddOrderDTO addOrderDTO, List<AddOrderItemDTO> addOrderItemDTOs);
    
    
        Task<IEnumerable<OrderListItemDTO>> GetOrders();
    }
}