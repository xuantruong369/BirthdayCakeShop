using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepo = customerRepository;
        }

        public async Task<IEnumerable<GetCustomerDTO>> GetCustomers()
        {
            var c = await _customerRepo.GetAllCustomers();
            List<GetCustomerDTO> customers = c.Select(item => new GetCustomerDTO
            {
                UserId = item.UserId,
                CustomerName = item.CustomerName,
                Phone = item.Phone,
                BirthDate = item.BirthDate,
                Address = item.Address,
                Avatar = item.Avatar,
                CustomerType = item.CustomerType,
                Username = item.User.Username,
                PasswordHash = item.User.PasswordHash,
                Role = item.User.Role,
                CreatedAt = item.User.CreatedAt
            }).ToList();
            return customers;
        }

        public async Task<GetCustomerDTO> GetCustomerByUserId(int? userId)
        {
            var customers = await _customerRepo.GetByUserId(userId);

            return new GetCustomerDTO
            {
                CustomerId = customers.CustomerId,
                UserId = customers.UserId,
                CustomerName = customers.CustomerName,
                Phone = customers.Phone,
                BirthDate = customers.BirthDate,
                Address = customers.Address,
                Avatar = customers.Avatar,
                CustomerType = customers.CustomerType,
                Username = customers.User.Username,
                PasswordHash = customers.User.PasswordHash,
                Role = customers.User.Role,
                CreatedAt = customers.User.CreatedAt
            };
        }

        public async Task<bool> UpdateCustomer(int customerId, AddCustomerDTO addCustomerDTO)
        {
            try
            {
                var customer = await _customerRepo.GetCustomerById(customerId);
                if (customer == null)
                {
                    return false;
                }

                // Update customer properties
                customer.CustomerName = addCustomerDTO.CustomerName;
                customer.Phone = addCustomerDTO.Phone;
                customer.BirthDate = addCustomerDTO.BirthDate;
                customer.Address = addCustomerDTO.Address;

                if (!string.IsNullOrEmpty(addCustomerDTO.Avatar))
                {
                    customer.Avatar = addCustomerDTO.Avatar;
                }

                await _customerRepo.Update(customer);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật khách hàng: " + ex.Message);
            }
        }

        public async Task<GetCustomerDTO> GetCustomerById(int customerId)
        {
            try
            {
                var customer = await _customerRepo.GetCustomerById(customerId);
                if (customer == null)
                {
                    return null;
                }

                return new GetCustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    UserId = customer.UserId,
                    CustomerName = customer.CustomerName,
                    Phone = customer.Phone,
                    BirthDate = customer.BirthDate,
                    Address = customer.Address,
                    Avatar = customer.Avatar,
                    CustomerType = customer.CustomerType,
                    Username = customer.User?.Username,
                    PasswordHash = customer.User?.PasswordHash,
                    Role = customer.User?.Role,
                    CreatedAt = customer.User?.CreatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin khách hàng: " + ex.Message);
            }
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            try
            {
                await _customerRepo.Delete(customerId);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa khách hàng: " + ex.Message);
            }
        }

        public async Task<AdminCustomerDTO> GetAdminCustomers()
        {
            var c = await _customerRepo.GetAllCustomers();
            List<GetCustomerDTO> customers = c.Select(item => new GetCustomerDTO
            {
                CustomerId = item.CustomerId,
                UserId = item.UserId,
                CustomerName = item.CustomerName,
                Phone = item.Phone,
                BirthDate = item.BirthDate,
                Address = item.Address,
                Avatar = item.Avatar,
                CustomerType = item.CustomerType,
                Username = item.User.Username,
                PasswordHash = item.User.PasswordHash,
                Role = item.User.Role,
                CreatedAt = item.User.CreatedAt
            }).ToList();
            var now = DateTime.Now;
            var customerresult = new AdminCustomerDTO
            {
                getCustomerDTOs = customers,
                TotalCustomers = c.Count(),
                TotalTypeCustomers = c.Count(p => p.CustomerType == "Vip"),
                TotalNewCustomers = c.Count(p => p.User.CreatedAt?.Month == now.Month && p.User.CreatedAt?.Year == now.Year)
            };
            return customerresult;
        }

        public async Task DeleteAdminCustomer(int customerId)
        {
            await _customerRepo.DeleteCutomer(customerId);
        }

        public async Task<(IEnumerable<GetCustomerDTO> items, int total)> SearchAdCustomers(string search, string sex, string orderStatus, int page = 1, int pageSize = 10)
        {
            var customers = await _customerRepo.GetAllCustomers();

            // Filter theo search keyword (tên, email, SĐT)
            if (!string.IsNullOrWhiteSpace(search))
            {
                customers = customers.Where(c =>
                    c.CustomerName.ToLower().Contains(search.ToLower()) ||
                    c.Phone.Contains(search) ||
                    c.User.Username.ToLower().Contains(search.ToLower())
                ).ToList();
            }

            // Filter theo giới tính (sex)
            if (!string.IsNullOrWhiteSpace(sex))
            {
                // Giả sử bạn có field sex hoặc bạn có thể lọc theo CustomerType
                // Nếu không có, bỏ qua bước này hoặc thêm field sex vào entity
                // customers = customers.Where(c => c.Sex == sex).ToList();
            }

            // Filter theo trạng thái hoạt động
            if (!string.IsNullOrWhiteSpace(orderStatus))
            {
                if (orderStatus == "active")
                {
                    // Khách hàng có đơn hàng gần đây
                    // Cần kiểm tra database của bạn có bảng Order không
                    // Tạm thời bỏ qua
                }
                else if (orderStatus == "inactive")
                {
                    // Khách hàng không hoạt động
                    // Tạm thời bỏ qua
                }
            }

            // Tổng số khách hàng phù hợp
            int totalCount = customers.Count();

            // Phân trang
            var pagedCustomers = customers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Map sang DTO
            List<GetCustomerDTO> customerDTOs = pagedCustomers.Select(item => new GetCustomerDTO
            {
                CustomerId = item.CustomerId,
                UserId = item.UserId,
                CustomerName = item.CustomerName,
                Phone = item.Phone,
                BirthDate = item.BirthDate,
                Address = item.Address,
                Avatar = item.Avatar,
                CustomerType = item.CustomerType,
                Username = item.User.Username,
                PasswordHash = item.User.PasswordHash,
                Role = item.User.Role,
                CreatedAt = item.User.CreatedAt
            }).ToList();

            return (customerDTOs, totalCount);
        }
    }
}