using ProjectsManagementAPI.Dto.User;
using ProjectsManagementAPI.Models;

namespace ProjectsManagementAPI.Services.Auth
{
    public interface IAuthInterface
    {
        Task<ResponseModel<string>> Login(UserLoginDto userLogin);
        Task<ResponseModel<UserCreateDto>> Register(UserCreateDto userCreateDto);
    }
}
