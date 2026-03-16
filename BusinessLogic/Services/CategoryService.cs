using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }
        public async Task<IEnumerable<GetCategoryDTO>> GetCategorys()
        {
            var category = await _categoryRepo.GetAll();
            List<GetCategoryDTO> getCategoryDTOs = category.Select(item => new GetCategoryDTO
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName
            }).ToList();
            return getCategoryDTOs;
        }
    }
}