using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByProduct(int productId);
    }
}