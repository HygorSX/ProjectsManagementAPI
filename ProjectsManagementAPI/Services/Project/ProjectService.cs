using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectsManagementAPI.Data;
using ProjectsManagementAPI.Dto.Project;
using ProjectsManagementAPI.Models;

namespace ProjectsManagementAPI.Services.Project
{
    public class ProjectService : IProjectInterface
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseModel<ProjectModel>> ListProjectForId(int idProject)
        {
            ResponseModel<ProjectModel> response = new ResponseModel<ProjectModel>();

            var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == idProject);

            if (project == null)
            {
                throw new KeyNotFoundException("Project not found!");
            }

            response.Data = project;
            response.Message = "The project was collected";

            return response;
        }

        public async Task<ResponseModel<List<ProjectModel>>> ListProjectsForUser(string userId)
        {
            ResponseModel<List<ProjectModel>> response = new ResponseModel<List<ProjectModel>>();

            if (string.IsNullOrEmpty(userId))
            {
                throw new KeyNotFoundException("User not found!");
            }

            var projects = await _context.Projects
                .Where(p => p.User.Id == int.Parse(userId))
                .ToListAsync();

            response.Data = projects;
            response.Message = "All project were collected";

            return response;
        }

        public async Task<ResponseModel<List<ProjectModel>>> CreateProject(ProjectCreateDto projectCreateDto)
        {
            ResponseModel<List<ProjectModel>> response = new ResponseModel<List<ProjectModel>>();

            var user = await _context.Users
                .FirstOrDefaultAsync(userDb => userDb.Id == projectCreateDto.User.Id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            if (CheckProjectExist(projectCreateDto))
            {
                throw new InvalidOperationException("Project already exists!");
            }

            ProjectModel project = new ProjectModel()
            {
                Name = projectCreateDto.Name,
                User = user
            };

            _context.Add(project);
            await _context.SaveChangesAsync();

            response.Data = await _context.Projects.Include(u => u.User).ToListAsync();
            response.Message = "The project was created successfully";

            return response;
        }

        public async Task<ResponseModel<ProjectModel>> UpdateProject(ProjectUpdateDto projectUpdateDto)
        {
            ResponseModel<ProjectModel> response = new ResponseModel<ProjectModel>();

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectUpdateDto.Id);

            if (project == null)
            {
                throw new KeyNotFoundException("Project not found!");
            }

            project.Name = projectUpdateDto.Name;

            _context.Update(project);
            await _context.SaveChangesAsync();

            response.Data = project;
            response.Message = "The project was updated!";

            return response;
        }

        public async Task<ResponseModel<ProjectModel>> DeleteProject(int idProject)
        {
            ResponseModel<ProjectModel> response = new ResponseModel<ProjectModel>();

            var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == idProject);

            if (project == null)
            {
                throw new KeyNotFoundException("Project not found!");
            }
            ;

            _context.Remove(project);
            await _context.SaveChangesAsync();

            response.Message = "The project was deleted!";

            return response;
        }

        public bool CheckProjectExist(ProjectCreateDto projectCreate)
        {
            return _context.Projects
                .Any(u => u.User.Id == projectCreate.User.Id && u.Name == projectCreate.Name);
        }
    }
}
