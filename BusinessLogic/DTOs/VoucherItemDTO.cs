namespace BusinessLogic.DTOs
{
    public class VoucherItemDTO
    {
        public int VoucherId { get; set; }

        public string VoucherCode { get; set; } = null!;

        public decimal DiscountValue { get; set; }

        public bool? IsPercentage { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}