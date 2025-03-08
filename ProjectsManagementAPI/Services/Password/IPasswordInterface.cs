using ProjectsManagementAPI.Models;

namespace ProjectsManagementAPI.Services.Password
{
    public interface IPasswordInterface
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool PasshwordHashVerify(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(UserModel user);
    }
}
