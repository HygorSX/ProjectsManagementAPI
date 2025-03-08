using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagementAPI.Models.Enum;

namespace ProjectsManagementAPI.Dto.User
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
    }
}
