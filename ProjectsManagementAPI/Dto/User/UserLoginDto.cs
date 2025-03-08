using System.ComponentModel.DataAnnotations;

namespace ProjectsManagementAPI.Dto.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "The field email is required"), EmailAddress(ErrorMessage = "Email invalid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field password is required")]
        public string Password { get; set; }
    }
}
