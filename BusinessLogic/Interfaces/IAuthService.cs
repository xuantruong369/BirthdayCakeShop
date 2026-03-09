using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO?> Login(UserDTO userDTO);
    }
}