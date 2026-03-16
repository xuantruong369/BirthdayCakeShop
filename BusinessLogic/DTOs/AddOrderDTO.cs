namespace BusinessLogic.DTOs
{
    public class AddOrderDTO
    {
        public int? UserId { get; set; }

        public string? PaymentMethod { get; set; }

        public string? ShippingAddress { get; set; }

        public DateOnly? DeliveryDate { get; set; }

        public string? DeliveryTimeSlot { get; set; }

        public string? Note { get; set; }
    }
}