using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(BirthdayCakeShopDbContext context) : base(context) { }

        public async Task<Voucher?> GetByCode(string code)
        {
            return await _context.Vouchers
                .FirstOrDefaultAsync(x => x.VoucherCode == code);
        }
    }
}