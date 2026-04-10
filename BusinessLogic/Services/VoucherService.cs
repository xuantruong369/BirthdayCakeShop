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

        public Task<bool> CheckVoucherCodeExist(string voucherCode)
        {
            return _voucherRepo.CheckVoucherCodeExist(voucherCode);
        }

        public async Task DeleteVoucher(int voucherId)
        {
            await _voucherRepo.Delete(voucherId);
        }

        public async Task EditVoucher(VoucherItemDTO edit)
        {
            var voucher = await _voucherRepo.GetById(edit.VoucherId);
            if (voucher != null)
            {
                voucher.VoucherCode = edit.VoucherCode;
                voucher.DiscountValue = edit.DiscountValue;
                voucher.IsPercentage = edit.IsPercentage;
                voucher.StartDate = edit.StartDate;
                voucher.EndDate = edit.EndDate;

                await _voucherRepo.Update(voucher);
            }
            // await _voucherRepo.Update(new Voucher
            // {
            //     VoucherId = edit.VoucherId,
            //     VoucherCode = edit.VoucherCode,
            //     DiscountValue = edit.DiscountValue,
            //     IsPercentage = edit.IsPercentage,
            //     StartDate = edit.StartDate,
            //     EndDate = edit.EndDate
            // });
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

        public async Task<VoucherItemDTO> GetVoucherById(int voucherId)
        {
            var voucher = await _voucherRepo.GetById(voucherId);
            return new VoucherItemDTO
            {
                VoucherId = voucher.VoucherId,
                VoucherCode = voucher.VoucherCode,
                DiscountValue = voucher.DiscountValue,
                IsPercentage = voucher.IsPercentage,
                StartDate = voucher.StartDate,
                EndDate = voucher.EndDate
            };
        }

        public async Task<(IEnumerable<VoucherItemDTO> items, int total)> SearchAdVouchers(string search, string status, string type, int page = 1, int pageSize = 10)
        {
            var vouchers = await _voucherRepo.GetAllVouchers();
            var now = DateTime.Now;

            // Filter theo search keyword (mã voucher)
            if (!string.IsNullOrWhiteSpace(search))
            {
                vouchers = vouchers.Where(v => v.VoucherCode.ToLower().Contains(search.ToLower())).ToList();
            }

            // Filter theo trạng thái
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (status == "active")
                {
                    vouchers = vouchers.Where(v => v.EndDate >= now).ToList();
                }
                else if (status == "inactive")
                {
                    vouchers = vouchers.Where(v => v.EndDate < now && v.StartDate <= now).ToList();
                }
                else if (status == "expired")
                {
                    vouchers = vouchers.Where(v => v.EndDate < now).ToList();
                }
            }

            // Filter theo loại (phần trăm hoặc số tiền cố định)
            if (!string.IsNullOrWhiteSpace(type))
            {
                if (type == "percentage")
                {
                    vouchers = vouchers.Where(v => v.IsPercentage == true).ToList();
                }
                else if (type == "fixed")
                {
                    vouchers = vouchers.Where(v => v.IsPercentage == false).ToList();
                }
            }

            // Tổng số voucher phù hợp
            int totalCount = vouchers.Count();

            // Phân trang
            var pagedVouchers = vouchers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Map sang DTO
            List<VoucherItemDTO> voucherDTOs = pagedVouchers.Select(item => new VoucherItemDTO
            {
                VoucherId = item.VoucherId,
                VoucherCode = item.VoucherCode,
                DiscountValue = item.DiscountValue,
                IsPercentage = item.IsPercentage,
                StartDate = item.StartDate,
                EndDate = item.EndDate
            }).ToList();

            return (voucherDTOs, totalCount);
        }
    }
}