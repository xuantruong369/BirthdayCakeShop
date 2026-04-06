namespace Presentation.ViewModels
{
    public class CategoryMenuView
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public int TotalProducts { get; set; } = 0;
        public int TotalSold { get; set; } = 0;
    }
}