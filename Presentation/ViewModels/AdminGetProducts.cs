namespace Presentation.ViewModels
{
    public class AdminGetProducts
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Thumbnail { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;
        public decimal Price { get; set; }
        public int? Stock { get; set; }

    }
}