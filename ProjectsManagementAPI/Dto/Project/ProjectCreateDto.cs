using ProjectsManagementAPI.Dto.BondDto;

namespace ProjectsManagementAPI.Dto.Project
{
    public class ProjectCreateDto
    {
        public string Name { get; set; }

        public UserBondDto User { get; set; }
    }
}
