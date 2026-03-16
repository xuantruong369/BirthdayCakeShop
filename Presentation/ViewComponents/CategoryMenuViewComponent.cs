using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryMenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetCategorys();
            List<CategoryMenuView> categoryMenuViews = categories.Select(item => new CategoryMenuView
            {
               CategoryId = item.CategoryId,
               CategoryName = item.CategoryName 
            }).ToList();
            return View(categoryMenuViews);
        }
    }
}