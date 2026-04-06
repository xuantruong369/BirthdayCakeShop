using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
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

        public async Task Add(GetCategoryDTO getCategoryDTO)
        {
            await _categoryRepo.Add(new CakeCategory
            {
                CategoryName = getCategoryDTO.CategoryName,
                Description = getCategoryDTO.Description
            });
        }

        public async Task<IEnumerable<AdminCategoryDTO>> GetAdminCategorys()
        {
            var categories = await _categoryRepo.GetAllCategorys();
            List<AdminCategoryDTO> adminCategories = categories.Select(item => new AdminCategoryDTO
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Description = item.Description,
                TotalProducts = item.Products.Count(),
                TotalSold = item.Products
                    .SelectMany(p => p.ProductDetails)
                    .SelectMany(pd => pd.OrderItems)
                    .Sum(oi => (int?)oi.Quantity) ?? 0

            }).ToList();
            return adminCategories;
        }
        public async Task Update(GetCategoryDTO getCategoryDTO)
        {
            await _categoryRepo.Update(new CakeCategory
            {
                CategoryId = getCategoryDTO.CategoryId,
                CategoryName = getCategoryDTO.CategoryName,
                Description = getCategoryDTO.Description
            });
        }

        public async Task Delete(int id)
        {
            await _categoryRepo.Delete(id);
        }

        public async Task<GetCategoryDTO> GetCategoryById(int categoryId)
        {
            var category = await _categoryRepo.GetById(categoryId);
            return new GetCategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }
    }
}