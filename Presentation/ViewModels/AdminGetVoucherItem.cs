namespace Presentation.ViewModels
{
    public class AdminGetVoucherItem
    {
        public int VoucherId { get; set; }

        public string VoucherCode { get; set; } = null!;

        public decimal DiscountValue { get; set; }

        public bool? IsPercentage { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? strIsPercentage { get; set; }
        public string? strIsActive { get; set; }
    }
}