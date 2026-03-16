namespace Presentation.ViewModels
{
     public class OrderListItemViewModel
    {
        public int OrderId { get; set; }
        public string CustomerFullName { get; set; }
        public string Phone { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
        public DateOnly? DeliveryDate { get; set; }
        public string DeliveryTimeSlot { get; set; }
        public string? OrderNote { get; set; }
        public List<OrderProductItemViewModel> Products { get; set; } = new();
    }
}