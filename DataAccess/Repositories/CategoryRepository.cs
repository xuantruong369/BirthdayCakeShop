using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class CategoryRepository : Repository<CakeCategory>, ICategoryRepository
    {
        public CategoryRepository(BirthdayCakeShopDbContext context) : base(context) { }
    }
}