using InhlwathiTutors.Data;
using InhlwathiTutors.Models;
using Microsoft.EntityFrameworkCore;

public class TutorshipService : ITutorshipService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public TutorshipService(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<Tutorship> CreateTutorshipAsync(string userId, CreateTutorshipDto dto)
    {
        try
        {
            if (await _context.Tutorships.AnyAsync(t => t.SystemUserId == userId))
                throw new InvalidOperationException("This user already has a tutorship profile");

            string? savedPath = await SaveProfileImageAsync(dto.ProfilePhotoPath);

            var tutorship = new Tutorship
            {
                SystemUserId = userId,
                Bio = dto.Bio,
                Qualifications = dto.Qualifications,
                Achievements = dto.Achievements,
                IsAvailable = dto.IsAvailable,
                YearsOfExperience = dto.YearsOfExperience,
                CreatedAt = DateTime.UtcNow,
                Rating = 0,
                ProfilePhotoPath = savedPath
            };

            _context.Tutorships.Add(tutorship);
            await _context.SaveChangesAsync(); // tutorship.Id is now available

            var languages = dto.LanguageIds.Select(langId => new TutorshipLanguage
            {
                TutorshipId = tutorship.Id,
                LanguageId = langId
            }).ToList();

            _context.TutorshipLanguages.AddRange(languages);
            await _context.SaveChangesAsync();

            return tutorship;
        }
        catch (Exception ex)
        {
            // Optional: log error
            throw new ApplicationException("An error occurred while creating the tutorship profile.", ex);
        }
    }

    private async Task<string?> SaveProfileImageAsync(string? base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
            return null;

        var imageBytes = Convert.FromBase64String(base64);
        var fileName = $"tutor_{Guid.NewGuid()}.jpg";
        var filePath = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads", fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        await File.WriteAllBytesAsync(filePath, imageBytes);

        return Path.Combine("uploads", fileName).Replace("\\", "/");
    }

    public async Task<string> GetUserModeAsync(string userId)
    {
        var hasTutorship = await _context.Tutorships
            .AnyAsync(t => t.SystemUserId == userId);

        return hasTutorship ? "tutor" : "student";
    }

    public async Task<bool> ChangeUserModeAsync(string userId, string newMode)
    {
        var validModes = new[] { "student", "tutor" };

        if (!validModes.Contains(newMode.ToLower()))
            return false;

        if (newMode.ToLower() == "tutor")
        {
            var hasTutorship = await _context.Tutorships.AnyAsync(t => t.SystemUserId == userId);
            if (!hasTutorship)
                return false;
        }

        var user = await _context.SystemUsers.FindAsync(userId);
        if (user == null)
            return false;

        user.mode = newMode.ToLower();
        _context.SystemUsers.Update(user);
        await _context.SaveChangesAsync();

        return true;
    }


}
