namespace BusinessLogic.DTOs
{
    public class CartItemDTO
    {
        public int? ProductId {get; set;}
        public int? CartItemId {get; set;}
        public int? ProductDetailId {get; set;}
        public string ProductName { get; set; } = null!;
        public string? Thumbnail { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

    }
}