using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagementAPI.Dto.Project;
using ProjectsManagementAPI.Models;
using ProjectsManagementAPI.Services.Project;
using System.Security.Claims;

namespace ProjectsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectInterface _projectInterface;
        public ProjectController(IProjectInterface projectInterface)
        {
            _projectInterface = projectInterface;
        }


        [Authorize]
        [HttpGet("ListProjectForUser")]
        public async Task<ActionResult<ResponseModel<List<ProjectModel>>>> ListProjectForUser()
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;

                var projects = await _projectInterface.ListProjectsForUser(userId);

                return Ok(projects);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("ListProjectForId{idProject}")]
        public async Task<ActionResult<ResponseModel<ProjectModel>>> ListProjectForId(int idProject)
        {
            try
            {
                var project = await _projectInterface.ListProjectForId(idProject);

                return Ok(project);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("CreateProject")]
        public async Task<ActionResult<List<ProjectModel>>> CreateProject(ProjectCreateDto projectCreateDto)
        {
            try
            {
                var projects = await _projectInterface.CreateProject(projectCreateDto);

                return Created("ListProjectForUser", projects);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("UpdateProject")]
        public async Task<ActionResult<ResponseModel<ProjectModel>>> UpdateProject(ProjectUpdateDto projectUpdate)
        {
            try
            {
                var project = await _projectInterface.UpdateProject(projectUpdate);

                return Ok(project);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("DeleteProject")]
        public async Task<ActionResult<ResponseModel<ProjectModel>>> DeleteProject(int idProject)
        {
            try
            {
                var project = await _projectInterface.DeleteProject(idProject);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
