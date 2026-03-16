namespace BusinessLogic.DTOs
{
    public class AddOrderItemDTO
    {
        public int? ProductDetailId { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }
        public int CartItemId {get; set;}
    }
}