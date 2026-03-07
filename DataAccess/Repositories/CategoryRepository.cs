using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BirthdayCakeShopDbContext _context;

        public CategoryRepository(BirthdayCakeShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CakeCategory> GetAll()
        {
            return _context.CakeCategories.ToList();
        }

        public CakeCategory GetById(string id)
        {
            return _context.CakeCategories.Find(id);
        }

        public void Add(CakeCategory category)
        {
            _context.CakeCategories.Add(category);
            _context.SaveChanges();
        }

        public void Update(CakeCategory category)
        {
            _context.CakeCategories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var category = _context.CakeCategories.Find(id);

            if (category != null)
            {
                _context.CakeCategories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}