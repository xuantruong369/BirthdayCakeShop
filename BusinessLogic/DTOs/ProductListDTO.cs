namespace BusinessLogic.DTOs
{
    public class ProductListDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}