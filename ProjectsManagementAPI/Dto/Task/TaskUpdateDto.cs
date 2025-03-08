using ProjectsManagementAPI.Models.Enum;
using ProjectsManagementAPI.Models;
using ProjectsManagementAPI.Dto.BondDto;

namespace ProjectsManagementAPI.Dto.Task
{
    public class TaskUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusType Status { get; set; }

        public ProjectBondDto Project { get; set; }
    }
}
