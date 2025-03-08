using System.Text.Json.Serialization;

namespace ProjectsManagementAPI.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserModel User { get; set; }

        [JsonIgnore]
        public ICollection<TaskModel> Tasks { get; set; }
    }
}
