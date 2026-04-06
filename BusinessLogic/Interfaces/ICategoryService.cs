using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetCategorys();
        Task<IEnumerable<AdminCategoryDTO>> GetAdminCategorys();
        Task Add(GetCategoryDTO getCategoryDTO);
        Task Update(GetCategoryDTO getCategoryDTO);
        Task Delete(int id);
        Task<GetCategoryDTO> GetCategoryById(int categoryId);

    }
}