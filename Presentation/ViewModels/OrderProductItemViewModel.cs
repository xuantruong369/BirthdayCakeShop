namespace Presentation.ViewModels
{
    public class OrderProductItemViewModel
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice {get; set;}
    }
}