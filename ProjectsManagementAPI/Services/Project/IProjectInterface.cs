using ProjectsManagementAPI.Dto.Project;
using ProjectsManagementAPI.Models;

namespace ProjectsManagementAPI.Services.Project
{
    public interface IProjectInterface
    {
        Task<ResponseModel<List<ProjectModel>>> ListProjectsForUser(string userId);
        Task<ResponseModel<ProjectModel>> ListProjectForId(int idProject);
        Task<ResponseModel<List<ProjectModel>>> CreateProject(ProjectCreateDto projectCreateDto); 
        Task<ResponseModel<ProjectModel>> UpdateProject(ProjectUpdateDto projectUpdateDto); 
        Task<ResponseModel<ProjectModel>> DeleteProject(int idProject);
    }
}
