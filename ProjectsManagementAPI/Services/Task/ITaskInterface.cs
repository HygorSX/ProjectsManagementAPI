using ProjectsManagementAPI.Dto.Task;
using ProjectsManagementAPI.Models;

namespace ProjectsManagementAPI.Services.Task
{
    public interface ITaskInterface
    {
        Task<ResponseModel<List<TaskModel>>> ListTaskOfProject(int idProject);
        Task<ResponseModel<TaskModel>> ListTaskForId(int idTask);
        Task<ResponseModel<List<TaskModel>>> CreateTaskInProject(TaskCreateDto taskCreateDto, string userId); 
        Task<ResponseModel<TaskModel>> UpdateTask(TaskUpdateDto taskUpdateDto, string userId);
        Task<ResponseModel<TaskModel>> DeleteTask(int idTask, string userId);
    }
}
