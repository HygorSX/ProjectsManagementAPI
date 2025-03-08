using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using ProjectsManagementAPI.Data;
using ProjectsManagementAPI.Dto.Task;
using ProjectsManagementAPI.Models;
using System.Threading.Tasks;

namespace ProjectsManagementAPI.Services.Task
{
    public class TaskService : ITaskInterface
    {
        private readonly AppDbContext _context;
        public TaskService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseModel<TaskModel>> ListTaskForId(int idTask)
        {
            ResponseModel<TaskModel> response = new ResponseModel<TaskModel>();

            var task = await _context.Tasks
                .FirstOrDefaultAsync(p => p.Id == idTask);

            if (task == null)
            {
                throw new KeyNotFoundException("Task not found!");
            }

            response.Data = task;
            response.Message = "Task has been collected";

            return response;
        }

        public async Task<ResponseModel<List<TaskModel>>> ListTaskOfProject(int idProject)
        {
            ResponseModel<List<TaskModel>> response = new ResponseModel<List<TaskModel>>();

            var tasks = await _context.Tasks
                .Where(p => p.Project.Id == idProject)
                .ToListAsync();

            if (tasks == null)
            {
                throw new KeyNotFoundException("Tasks not found!");
            }

            response.Data = tasks;
            response.Message = "All project tasks have been collected";

            return response;
        }

        public async Task<ResponseModel<List<TaskModel>>> CreateTaskInProject(TaskCreateDto taskCreateDto, string userId)
        {
            ResponseModel<List<TaskModel>> response = new ResponseModel<List<TaskModel>>();

            var project = await _context.Projects
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == taskCreateDto.Project.Id);

            if (project == null)
            {
                throw new KeyNotFoundException("Project not found!");
            }

            if (project.User.Id != int.Parse(userId))
            {
                throw new UnauthorizedAccessException("You are not the project administrator");
            }

            if (CheckTaskLimit(taskCreateDto.Project.Id))
            {
                throw new InvalidOperationException("The project has reached the maximum number of tasks.");
            }

            TaskModel task = new TaskModel()
            {
                Name = taskCreateDto.Name,
                Description = taskCreateDto.Description,
                Project = project,
                Status = Models.Enum.StatusType.Pendente
            };

            _context.Add(task);
            await _context.SaveChangesAsync();

            response.Data = await _context.Tasks.Include(u => u.Project).ToListAsync();
            response.Message = "The task was created successfully";

            return response;
        }

        public async Task<ResponseModel<TaskModel>> DeleteTask(int idTask, string userId)
        {
            ResponseModel<TaskModel> response = new ResponseModel<TaskModel>();

            var task = await _context.Tasks
                .Include(p => p.Project)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == idTask);

            if(task == null)
            {
                throw new KeyNotFoundException("Task not found!");
            }

            if (task.Project.User.Id != int.Parse(userId))
            {
                throw new UnauthorizedAccessException("You are not the project administrator");
            }

            _context.Remove(task);
            await _context.SaveChangesAsync();

            response.Message = "The task was deleted!";

            return response;
        }

        public async Task<ResponseModel<TaskModel>> UpdateTask(TaskUpdateDto taskUpdateDto, string userId)
        {
            ResponseModel<TaskModel> response = new ResponseModel<TaskModel>();

            var task = await _context.Tasks
                .Include(p => p.Project)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == taskUpdateDto.Id);

            if(task == null)
            {
                throw new KeyNotFoundException("Task not found!");
            }

            if (task.Project.User.Id != int.Parse(userId))
            {
                throw new UnauthorizedAccessException("You are not the project administrator");
            }

            task.Name = taskUpdateDto.Name;
            task.Description = taskUpdateDto.Description;
            task.Status = taskUpdateDto.Status;

            _context.Update(task);
            await _context.SaveChangesAsync();

            response.Data = task;
            response.Message = "The task was updated!";

            return response;
        }

        public bool CheckTaskLimit(int idProject)
        {
            int taskCount = _context.Tasks
                .Where(p => p.Project.Id == idProject && p.Status == Models.Enum.StatusType.Pendente)
                .Count();

            return taskCount == 5;
        }
    }
}
