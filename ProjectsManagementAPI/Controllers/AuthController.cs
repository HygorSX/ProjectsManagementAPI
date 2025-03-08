using Microsoft.AspNetCore.Mvc;
using ProjectsManagementAPI.Dto.User;
using ProjectsManagementAPI.Services.Auth;

namespace ProjectsManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;
        public AuthController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var response = await _authInterface.Login(userLogin);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserCreateDto userRegister)
        {
            try
            {
                var response = await _authInterface.Register(userRegister);

                return Created("LisAuth", response);
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
    }
}
