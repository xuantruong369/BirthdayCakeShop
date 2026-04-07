namespace Presentation.ViewModels
{
    public class AdminGetVouchers
    {
        public List<AdminGetVoucherItem> adminGetVoucherItems { get; set; } = new();
        public int TotalVoucherActive { get; set; } = 0;
        public int TotalVoucherNoActive { get; set; } = 0;
    }
}