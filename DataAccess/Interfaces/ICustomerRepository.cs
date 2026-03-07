using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();

        Customer GetById(string id);

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(string id);
    }
}