using ProjectsManagementAPI.Dto.User;
using ProjectsManagementAPI.Models;

namespace ProjectsManagementAPI.Services.User
{
    public interface IUserInterface
    {
        Task<ResponseModel<List<UserListDto>>> ListUsers();
    }
}
