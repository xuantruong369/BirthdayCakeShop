using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface ICustomerService
    {
        Task<GetCustomerDTO> GetCustomerByUserId(int? userId);
        Task<GetCustomerDTO> GetCustomerById(int customerId);
        Task<bool> UpdateCustomer(int customerId, AddCustomerDTO addCustomerDTO);
        Task<bool> DeleteCustomer(int customerId);
        Task<IEnumerable<GetCustomerDTO>> GetCustomers();
    }
}