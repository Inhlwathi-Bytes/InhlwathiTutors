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
                return Ok();
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

        [HttpGet("my-profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            var profile = await _tutorshipService.GetProfileAsync(userId);
            if (profile == null)
                return NotFound("Tutorship profile not found.");

            return Ok(profile);
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetTutorProfile(string userId)
        {
            var profile = await _tutorshipService.GetProfileAsync(userId);
            if (profile == null)
                return NotFound("Tutorship profile not found.");

            return Ok(profile);
        }
    }
}

public class CreateTutorshipDto
{
    [Required]
    public string Bio { get; set; }
    public int Age { get; set; }
    public string HighestAchievement { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int YearsOfExperience { get; set; }
    public string? ProfilePhotoPath { get; set; }
    public List<int> LanguageIds { get; set; } = new();
}

public class ChangeModeRequest
{
    public string NewMode { get; set; } = string.Empty;
}

public class TutorshipDto
{
    // From Tutorship
    public int Id { get; set; }
    public string SystemUserId { get; set; }
    public string Bio { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public string HighestAchievement { get; set; }
    public bool IsAvailable { get; set; }
    public int YearsOfExperience { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
    public double Rating { get; set; }
    public string? ProfilePhotoPath { get; set; }

    // Flattened SystemUser data
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    // Subjects
    public List<TutorshipSubjectDto> TutorshipSubjects { get; set; } = new();

    // Languages
    public List<TutorshipLanguageDto> TutorshipLanguages { get; set; } = new();
}

public class TutorshipSubjectDto
{
    public string Name { get; set; }
    public string Availability { get; set; }
    public string Outline { get; set; }
}

public class TutorshipLanguageDto
{
    public string LanguageName { get; set; }
    public string ProficiencyLevel { get; set; }
}

