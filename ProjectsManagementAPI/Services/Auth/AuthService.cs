using Microsoft.EntityFrameworkCore;
using ProjectsManagementAPI.Data;
using ProjectsManagementAPI.Dto.User;
using ProjectsManagementAPI.Models;
using ProjectsManagementAPI.Services.Password;

namespace ProjectsManagementAPI.Services.Auth
{
    public class AuthService : IAuthInterface
    {
		private readonly AppDbContext _context;
		private readonly IPasswordInterface _passwordInterface;
        public AuthService(AppDbContext context, IPasswordInterface passwordInterface)
        {
			_context = context;
			_passwordInterface = passwordInterface;
        }


        public async Task<ResponseModel<UserCreateDto>> Register(UserCreateDto userRegister)
        {
            ResponseModel<UserCreateDto> response = new ResponseModel<UserCreateDto>();

            if (!CheckIfEmailAndUserExist(userRegister))
            {
                throw new InvalidOperationException("Email/User already registered");
            }

            _passwordInterface.CreatePasswordHash(userRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UserModel user = new UserModel()
            {
                User = userRegister.User,
                Email = userRegister.Email,
                Role = userRegister.Role,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            response.Message = "User created successfully";

            return response;
        }

        public async Task<ResponseModel<string>> Login(UserLoginDto userLogin)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            var user = await _context.Users.FirstOrDefaultAsync(userDb => userDb.Email == userLogin.Email);

            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist!");
            }

            if (!_passwordInterface.PasshwordHashVerify(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadHttpRequestException("Invalid information!");
            }

            var token = _passwordInterface.CreateToken(user);

            response.Message = "User logged in successfully!";
            response.Data = token;

            return response;
        }

        public bool CheckIfEmailAndUserExist(UserCreateDto userRegister)
		{
			var user = _context.Users.FirstOrDefault(userDb => userDb.Email == userRegister.Email || userDb.User == userRegister.User);

			if (user != null) return false;

			return true;
		}
    }
}
