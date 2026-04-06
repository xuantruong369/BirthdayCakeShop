using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface ICategoryRepository : IRepository<CakeCategory>
    {
        Task<IEnumerable<CakeCategory>> GetAllCategorys();
    }
}