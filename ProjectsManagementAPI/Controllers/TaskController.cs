using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagementAPI.Dto.Project;
using ProjectsManagementAPI.Dto.Task;
using ProjectsManagementAPI.Models;
using ProjectsManagementAPI.Services.Project;
using ProjectsManagementAPI.Services.Task;

namespace ProjectsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskInterface _taskInterface;
        public TaskController(ITaskInterface taskInterface)
        {
            _taskInterface = taskInterface;
        }


        [HttpGet("ListTaskOfProject{idProject}")] 
        public async Task<ActionResult<ResponseModel<List<TaskModel>>>> ListTaskOfProject(int idProject)
        {
            try
            {
                var tasks = await _taskInterface.ListTaskOfProject(idProject);

                return Ok(tasks);
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

        [HttpGet("ListTaskForId{idTask}")]
        public async Task<ActionResult<ResponseModel<TaskModel>>> ListProjectForId(int idTask)
        {
            try
            {
                var task = await _taskInterface
                .ListTaskForId(idTask);

                return Ok(task);
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

        [Authorize]
        [HttpPost("CreateTaskInProject")]
        public async Task<ActionResult<ResponseModel<List<TaskModel>>>> CreateTaskInProject(TaskCreateDto taskCreateDto)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;

                var task = await _taskInterface.CreateTaskInProject(taskCreateDto, userId);

                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
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

        [Authorize]
        [HttpPut("UpdateTask")]
        public async Task<ActionResult<ResponseModel<TaskModel>>> UpdateTask(TaskUpdateDto taskUpdateDto)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;

                var project = await _taskInterface.UpdateTask(taskUpdateDto, userId);

                return Ok(project);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("DeleteTask")]
        public async Task<ActionResult<ResponseModel<TaskModel>>> DeleteTask(int idTask)
        {
            try
            {
                var userId = User.FindFirst("UserId")?.Value;

                var project = await _taskInterface.DeleteTask(idTask, userId);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
