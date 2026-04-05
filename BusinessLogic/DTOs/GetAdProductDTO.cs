namespace BusinessLogic.DTOs
{
    public class GetAdProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string CategoryName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CategoryId { get; set; }
        public int? Stock { get; set; }
    }
}