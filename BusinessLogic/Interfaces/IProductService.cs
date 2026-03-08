using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
    }
}