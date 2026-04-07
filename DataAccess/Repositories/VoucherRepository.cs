using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<bool> CheckVoucherCodeExist(string voucherCode)
        {
            if (string.IsNullOrWhiteSpace(voucherCode)) return false;
            // Sử dụng AnyAsync để kiểm tra sự tồn tại (hiệu năng tốt hơn tải dữ liệu về)
            // Dùng ToUpper() để kiểm tra không phân biệt chữ hoa chữ thường
            return await _context.Vouchers
                .AnyAsync(v => v.VoucherCode.ToUpper() == voucherCode.Trim().ToUpper());

        }

        public async Task<IEnumerable<Voucher>> GetAllVouchers()
        {
            return await _context.Vouchers
                .Include(p => p.Orders)
                .ToListAsync();
        }

        public async Task<Voucher?> GetByCode(string code)
        {
            return await _context.Vouchers
                .FirstOrDefaultAsync(x => x.VoucherCode == code);
        }
    }
}