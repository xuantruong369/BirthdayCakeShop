using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO?> Login(UserDTO userDTO)
        {
            var user = await _userRepository.GetByUsername(userDTO.Username);

            if (user != null && user.PasswordHash == userDTO.PasswordHash)
            {
                return new UserDTO
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    PasswordHash = user.PasswordHash,
                    Role = user.Role
                };
            }
            return null;
        }
    }
}