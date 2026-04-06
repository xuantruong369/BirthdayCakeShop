namespace BusinessLogic.DTOs
{
    public class AdminCategoryDTO
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }
        public int TotalProducts { get; set; } = 0;
        public int TotalSold { get; set; } = 0;
    }
}