using Microsoft.EntityFrameworkCore;
using ProjectsManagementAPI.Data;
using ProjectsManagementAPI.Dto.User;
using ProjectsManagementAPI.Models;

namespace ProjectsManagementAPI.Services.User
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseModel<List<UserListDto>>> ListUsers()
        {
            ResponseModel<List<UserListDto>> response = new ResponseModel<List<UserListDto>>();

            var users = await _context.Users.ToListAsync();

            var usersDto = users.Select(u => new UserListDto
            {
                Id = u.Id,
                User = u.User,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            response.Data = usersDto;
            response.Message = "All users were collected";

            return response;
        }
    }
}
