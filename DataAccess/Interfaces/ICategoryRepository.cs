using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<CakeCategory> GetAll();

        CakeCategory GetById(string id);

        void Add(CakeCategory category);

        void Update(CakeCategory category);

        void Delete(string id);
    }
}