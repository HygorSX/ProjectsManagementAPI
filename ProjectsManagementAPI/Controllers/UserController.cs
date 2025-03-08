using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsManagementAPI.Dto.User;
using ProjectsManagementAPI.Models;
using ProjectsManagementAPI.Services.User;

namespace ProjectsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        
        [Authorize]
        [HttpGet("ListUsers")]
        public async Task<ActionResult<ResponseModel<UserModel>>> GetUser()
        {
            try
            {
                var users = await _userInterface.ListUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
