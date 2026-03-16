using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepo;
        private readonly ICustomerRepository _customserRepo;

        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _userRepo = userRepository;
            _customserRepo = customerRepository;
        }

        public async Task<RegisterResultDTO> Register(AddUserDTO addUserDTO)
        {
            // 1. Kiểm tra trùng tên
            if (await _userRepo.UsernameExists(addUserDTO.Username))
            {
                return new RegisterResultDTO { Success = false, Field = "Username", Message = "Tên đăng nhập đã tồn tại" };
            }

            // 2. Kiểm tra mật khẩu (Nên dùng Equals để an toàn hơn)
            if (addUserDTO.PasswordHash != addUserDTO.ConfirmPassword)
            {
                return new RegisterResultDTO { Success = false, Field = "ConfirmPassword", Message = "Mật khẩu xác nhận không khớp" };
            }

            // 3. Tạo object User
            var newUser = new User
            {
                Username = addUserDTO.Username,
                PasswordHash = addUserDTO.PasswordHash, // Lưu ý: Nên Hash mật khẩu, không nên để plain text
                Role = addUserDTO.Role
            };

            // 4. Lưu User vào DB trước để lấy ID
            await _userRepo.Add(newUser); 
            // Giả sử trong hàm Add của bạn đã có _context.SaveChangesAsync()
            // Sau dòng này, newUser.Id sẽ tự động có giá trị từ DB.

            // 5. Tạo Customer dùng ID vừa có
            var newCustomer = new Customer
            {
                UserId = newUser.UserId, // Dùng trực tiếp ID từ object vừa lưu
                CustomerName = addUserDTO.CustomerName,
                Phone = addUserDTO.Phone,
                Address = addUserDTO.Address,
            };

            await _customserRepo.Add(newCustomer);

            return new RegisterResultDTO { Success = true };
        }

    }
}