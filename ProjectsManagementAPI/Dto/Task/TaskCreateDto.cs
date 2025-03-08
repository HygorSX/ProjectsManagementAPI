using ProjectsManagementAPI.Models.Enum;
using ProjectsManagementAPI.Models;
using ProjectsManagementAPI.Dto.BondDto;

namespace ProjectsManagementAPI.Dto.Task
{
    public class TaskCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusType Status { get; set; } = StatusType.Pendente;

        public ProjectBondDto Project { get; set; }
    }
}
