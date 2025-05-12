using InhlwathiTutors.Models;
using InhlwathiTutors.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InhlwathiTutors.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var user = new SystemUser
            {
                UserName = model.Email,
                Email = model.Email,
                StudentNumber = model.StudentNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Location = model.Location,
                mode = "student"
            };

            try
            {
                var (result, token) = await _authService.RegisterWithTokenAsync(user, model.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user.", detail = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.LoginAndGenerateTokenAsync(request.Email, request.Password);
                if (token == null)
                    return Unauthorized(new { Message = "Invalid email or password" });

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
