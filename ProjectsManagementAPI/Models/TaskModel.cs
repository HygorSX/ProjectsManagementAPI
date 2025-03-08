using ProjectsManagementAPI.Models.Enum;

namespace ProjectsManagementAPI.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusType Status { get; set; } = StatusType.Pendente;

        public ProjectModel Project { get; set; }
    }
}
