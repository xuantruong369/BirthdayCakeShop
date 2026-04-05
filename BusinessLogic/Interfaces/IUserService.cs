using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResultDTO> Register(AddUserDTO addUserDTO);
        Task<UserDTO> GetUserById(int? userId);
        Task UpdatePassword(UserDTO userDTO);
    }
}