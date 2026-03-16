namespace BusinessLogic.DTOs
{
    public class GetProductDetailDTO
    {
        public int Id {get; set;}
        public int ProductId {get; set;}
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string? Thumbnail { get; set; }

        public string CategoryName { get; set; } = null!;

        public List<string?> ImageUrls {get; set;} = new List<string?>();
        public string? CakeSize { get; set; }

        public string? Flavor { get; set; }

        public decimal Price { get; set; }
        
    }
}
