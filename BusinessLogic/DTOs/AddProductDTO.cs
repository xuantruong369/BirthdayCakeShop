namespace BusinessLogic.DTOs
{
    public class AddProductDTO
    {
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Thumbnail { get; set; }
        public List<string> Images { get; set; } = new();
        public decimal Price { get; set; }
        public string? CakeSize { get; set; }
        public string? Flavor { get; set; }
    }
}