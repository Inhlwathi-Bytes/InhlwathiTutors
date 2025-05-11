using InhlwathiTutors.Models;
using InhlwathiTutors.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace InhlwathiTutors.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TutorshipController : ControllerBase
    {
        private readonly ITutorshipService _tutorshipService;
        private readonly UserManager<SystemUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public TutorshipController(ITutorshipService tutorshipService, UserManager<SystemUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _tutorshipService = tutorshipService;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTutorship([FromBody] CreateTutorshipDto dto)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized("User not authenticated");

            try
            {
                var tutorship = await _tutorshipService.CreateTutorshipAsync(userId, dto);
                return Ok(tutorship);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("has-tutorship")]
        public async Task<IActionResult> GetUserMode()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var mode = await _tutorshipService.GetUserModeAsync(userId);
            return Ok(new { mode });
        }

        [HttpPut("change")]
        [Authorize]
        public async Task<IActionResult> ChangeMode([FromBody] ChangeModeRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var success = await _tutorshipService.ChangeUserModeAsync(userId, request.NewMode);

            if (!success)
                return BadRequest("Invalid mode or conditions not met (e.g., no tutor profile).");

            return Ok(new { message = $"Mode changed to {request.NewMode}" });
        }
    }
}

public class CreateTutorshipDto
{
    [Required]
    public string Bio { get; set; }

    public string Qualifications { get; set; }

    public string Achievements { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int YearsOfExperience { get; set; }

    public string? ProfilePhotoPath { get; set; }

    public List<int> LanguageIds { get; set; } = new();
}

public class ChangeModeRequest
{
    public string NewMode { get; set; } = string.Empty;
}
