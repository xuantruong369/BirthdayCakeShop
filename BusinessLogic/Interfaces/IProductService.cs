using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDTO>> GetProducts();
        Task<GetProductDetailDTO> GetProductDetail(int id);
        Task AddProduct(AddProductDTO addProductDTO);
        // Task DeleteProduct(int productID);
        Task<IEnumerable<GetAdProductDTO>> GetAdProducts();
        Task DeleteProduct(int productId);
        Task<IEnumerable<ProductListDTO>> GetByCategoryId(int id);
        Task<AdminProductDTO> GetAllProductById(int productId);
        Task UpdateAllProduct(AdminProductDTO edit);
    }
}