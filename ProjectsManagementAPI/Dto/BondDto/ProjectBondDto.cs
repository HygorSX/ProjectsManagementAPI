namespace ProjectsManagementAPI.Dto.BondDto
{
    public class ProjectBondDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserBondDto User { get; set; }
    }
}
