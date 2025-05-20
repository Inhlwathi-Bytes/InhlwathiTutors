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

        [HttpPost("subject")]
        public async Task<IActionResult> CreateSubject([FromBody] CreateTutorshipSubjectDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            try
            {
                var created = await _tutorshipService.CreateTutorshipSubjectAsync(dto, user.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("subjects/explore")]
        public async Task<IActionResult> GetSubjects([FromQuery] int? tutorshipId = null)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            var subjects = await _tutorshipService.GetAllExploreSubjectsAsync();
            var response = new SubjectExploreResponse { Subjects = subjects };
            return Ok(response);
        }

    }
}

public class SubjectExploreResponse
{
    public List<SubjectExploreDto> Subjects { get; set; }
}
public class MyOfferedSubjectsResponse
{
    public List<TutorshipSubjectDto> Subjects { get; set; }
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
    public int Id { get; set; }
    public string SubjectName { get; set; }
    public string Availability { get; set; }
    public string Outline { get; set; }
    public string DeliveryMode { get; set; }
    public decimal HourlyRate { get; set; }
    public string Level { get; set; }
    public string? CoverImagePath { get; set; }
    public string? IntroVideoLink { get; set; }

    public List<SubjectLanguageDto> Languages { get; set; }
}

public class SubjectLanguageDto
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string LanguageName { get; set; } // Comes from `Language.Name`
}


public class TutorshipLanguageDto
{
    public string LanguageName { get; set; }
}

public class CreateTutorshipSubjectDto
{
    [Required]
    public string SubjectName { get; set; }

    [Required]
    public string Availability { get; set; }

    [Required]
    public string Outline { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal HourlyRate { get; set; }

    [Required]
    public string Level { get; set; }


    public string DeliveryMode { get; set; }

    public string? CoverImagePath { get; set; }

    public string? IntroVideoLink { get; set; }

    public List<int>? LanguageIds { get; set; }  // Optional: if you want to link languages
}

public class SubjectExploreDto
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public string Outline { get; set; }
    public string Availability { get; set; }
    public string Level { get; set; }
    public decimal HourlyRate { get; set; }

    // Tutor info
    public string TutorName { get; set; }
    public string TutorEmail { get; set; }
    public string ProfilePhotoPath { get; set; }

    // Language info (as a list)
    public List<string> LanguageNames { get; set; }

    // Ratings
    public double? AverageRating { get; set; }
}

