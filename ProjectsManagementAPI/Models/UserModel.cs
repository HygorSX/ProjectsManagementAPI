using ProjectsManagementAPI.Models.Enum;
using System.Text.Json.Serialization;

namespace ProjectsManagementAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }

        [JsonIgnore]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }

        [JsonIgnore]
        public DateTime TokenDataCreate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<ProjectModel>? Projects { get; set; }
    }
}
