namespace Presentation.ViewModels
{
    public class AdminOrderListDash
    {
        public int OrderId { get; set; }
        public string CustomerFullName { get; set; }
        public string Phone { get; set; }
        public decimal? TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
        public DateOnly? DeliveryDate { get; set; }
    }
}