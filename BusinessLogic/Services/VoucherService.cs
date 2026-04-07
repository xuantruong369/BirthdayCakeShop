using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Entities;

namespace BusinessLogic.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepo;
        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepo = voucherRepository;
        }

        public async Task AddVoucher(VoucherItemDTO add)
        {
            await _voucherRepo.Add(new Voucher
            {
                VoucherCode = add.VoucherCode,
                DiscountValue = add.DiscountValue,
                IsPercentage = add.IsPercentage,
                StartDate = add.StartDate,
                EndDate = add.EndDate
            });
        }

        public async Task DeleteVoucher(int voucherId)
        {
            await _voucherRepo.Delete(voucherId);
        }

        public async Task EditVoucher(VoucherItemDTO edit)
        {
            await _voucherRepo.Update(new Voucher
            {
                VoucherId = edit.VoucherId,
                VoucherCode = edit.VoucherCode,
                DiscountValue = edit.DiscountValue,
                IsPercentage = edit.IsPercentage,
                StartDate = edit.StartDate,
                EndDate = edit.EndDate
            });
        }

        public async Task<VoucherDTO> GetAllVouchers()
        {
            var vouchers = await _voucherRepo.GetAllVouchers();
            List<VoucherItemDTO> voucherItems = vouchers.Select(item => new VoucherItemDTO
            {
                VoucherId = item.VoucherId,
                VoucherCode = item.VoucherCode,
                DiscountValue = item.DiscountValue,
                IsPercentage = item.IsPercentage,
                StartDate = item.StartDate,
                EndDate = item.EndDate
            }).ToList();
            var now = DateTime.Now;
            var voucherDto = new VoucherDTO
            {
                voucherItemDTOs = voucherItems,
                TotalVoucherActive = vouchers.Count(p => p.EndDate >= now),
                TotalVoucherNoActive = vouchers.Count(p => p.EndDate < now)
            };
            return voucherDto;
        }
    }
}