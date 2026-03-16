using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
       Task<RegisterResultDTO> Register(AddUserDTO addUserDTO);
    }
}