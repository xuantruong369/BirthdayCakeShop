using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> GetByCode(string code);
    }
}