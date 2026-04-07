namespace BusinessLogic.DTOs
{
    public class VoucherDTO
    {
        public List<VoucherItemDTO> voucherItemDTOs { get; set; } = new();
        public int TotalVoucherActive { get; set; } = 0;
        public int TotalVoucherNoActive { get; set; } = 0;
    }
}