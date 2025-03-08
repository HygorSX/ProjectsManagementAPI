using ProjectsManagementAPI.Dto.BondDto;

namespace ProjectsManagementAPI.Dto.Project
{
    public class ProjectUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserBondDto User { get; set; }
    }
}
