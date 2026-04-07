using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IVoucherService
    {
        Task<VoucherDTO> GetAllVouchers();
        Task AddVoucher(VoucherItemDTO add);
        Task EditVoucher(VoucherItemDTO edit);
        Task DeleteVoucher(int voucherId);
        Task<bool> CheckVoucherCodeExist(string voucherCode);
        Task<VoucherItemDTO> GetVoucherById(int voucherId);
    }
}
