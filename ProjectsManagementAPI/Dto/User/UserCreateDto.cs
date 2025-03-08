using ProjectsManagementAPI.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProjectsManagementAPI.Dto.User
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "The field user is required")]
        public string User { get; set; }

        [Required(ErrorMessage = "The field email is required"), EmailAddress(ErrorMessage = "Email invalid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "The field role is required")]
        public RoleType Role { get; set; }
    }
}
