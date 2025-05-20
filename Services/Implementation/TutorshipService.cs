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
                Age = dto.Age,
                Street = dto.Street,
                City = dto.City,
                Province = dto.Province,
                PostalCode = dto.PostalCode,
                HighestAchievement = dto.HighestAchievement,
                IsAvailable = dto.IsAvailable,
                YearsOfExperience = dto.YearsOfExperience,
                CreatedAt = DateTime.UtcNow,
                Rating = 0,
                ProfilePhotoPath = savedPath
            };

            _context.Tutorships.Add(tutorship);
            await _context.SaveChangesAsync(); 

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

    public async Task<TutorshipDto?> GetProfileAsync(string userId)
    {
        var tutorship = await _context.Tutorships
            .Include(t => t.SystemUser)
            .Include(t => t.TutorshipSubjects)
            .Include(t => t.TutorshipLanguages)
            .FirstOrDefaultAsync(t => t.SystemUserId == userId);

        if (tutorship == null)
            return null;

        TutorshipDto newTutorshipDto =  new TutorshipDto
        {
            Id = tutorship.Id,
            SystemUserId = tutorship.SystemUserId,
            Bio = tutorship.Bio,
            Street = tutorship.Street,
            City = tutorship.City,
            Province = tutorship.Province,
            PostalCode = tutorship.PostalCode,
            HighestAchievement = tutorship.HighestAchievement,
            IsAvailable = tutorship.IsAvailable,
            YearsOfExperience = tutorship.YearsOfExperience,
            Age = tutorship.Age,
            CreatedAt = tutorship.CreatedAt,
            Rating = tutorship.Rating,
            Name = tutorship.SystemUser.FirstName,
            Surname = tutorship.SystemUser.LastName,
            Email = tutorship.SystemUser.Email,
            TutorshipSubjects = tutorship.TutorshipSubjects.Select(s => new TutorshipSubjectDto
            {
                SubjectName = s.SubjectName,
                Availability = s.Availability,
                Outline = s.Outline
            }).ToList(),

            TutorshipLanguages = tutorship.TutorshipLanguages
    .Where(l => l.Language != null) // Optional, skip nulls
    .Select(l => new TutorshipLanguageDto
    {
        LanguageName = l.Language?.Name ?? "Unknown"
    })
    .ToList()

        };
        if (tutorship.ProfilePhotoPath != null)
        {
            newTutorshipDto.ProfilePhotoPath = ConvertImageToBase64(
                Path.Combine(_environment.WebRootPath, tutorship.ProfilePhotoPath)
            );
        }

        return newTutorshipDto;
    }

    public async Task<TutorshipSubject> CreateTutorshipSubjectAsync(CreateTutorshipSubjectDto dto, string userId)
    {
        var tutorship = await _context.Tutorships
            .FirstOrDefaultAsync(t => t.SystemUserId == userId);

        if (tutorship == null)
            throw new Exception("No tutorship profile found for the user.");

        var subject = new TutorshipSubject
        {
            SubjectName = dto.SubjectName,
            Availability = dto.Availability,
            DeliveryMode = dto.DeliveryMode,
            Outline = dto.Outline,
            HourlyRate = dto.HourlyRate,
            Level = dto.Level,
            CoverImagePath = dto.CoverImagePath,
            IntroVideoLink = dto.IntroVideoLink,
            TutorshipId = tutorship.Id
        };

        // Optional: handle LanguageIds if applicable
        if (dto.LanguageIds != null)
        {
            subject.TutorshipSubjectLanguages = dto.LanguageIds
                .Select(langId => new TutorshipSubjectLanguage
                {
                    LanguageId = langId,
                    TutorshipSubjectId = tutorship.Id,
                }).ToList();
        }

        _context.TutorshipSubjects.Add(subject);
        await _context.SaveChangesAsync();

        return subject;
    }

    public string ConvertImageToBase64(string imagePath)
    {
        try
        {
        byte[] imageBytes = File.ReadAllBytes(imagePath); // Read image from file system
            string base64String = Convert.ToBase64String(imageBytes); // Convert to Base64 string
            return base64String;
        } catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<TutorshipSubjectDto>> GetSubjectsAsync(int? optionalTutorshipId, string userId)
    {
        int tutorshipId;

        if (optionalTutorshipId.HasValue)
        {
            tutorshipId = optionalTutorshipId.Value;
        }
        else
        {
            var tutorship = await _context.Tutorships
                .FirstOrDefaultAsync(t => t.SystemUserId == userId);

            if (tutorship == null) return new List<TutorshipSubjectDto>();

            tutorshipId = tutorship.Id;
        }

        var subjects = await _context.TutorshipSubjects
            .Where(s => s.TutorshipId == tutorshipId)
            .Include(s => s.TutorshipSubjectLanguages)
                .ThenInclude(tsl => tsl.Language)
            .ToListAsync();

        var dtos = subjects.Select(s => new TutorshipSubjectDto
        {
            Id = s.Id,
            SubjectName = s.SubjectName,
            DeliveryMode = s.DeliveryMode,
            Availability = s.Availability,
            Outline = s.Outline,
            HourlyRate = s.HourlyRate,
            Level = s.Level,
            CoverImagePath = s.CoverImagePath,
            IntroVideoLink = s.IntroVideoLink,
            Languages = s.TutorshipSubjectLanguages.Select(tsl => new SubjectLanguageDto
            {
                Id = tsl.Id,
                LanguageId = tsl.LanguageId,
                LanguageName = tsl.Language.Name
            }).ToList()
        }).ToList();

        return dtos;
    }

    public async Task<List<SubjectExploreDto>> GetAllExploreSubjectsAsync()
    {
        var subjects = await _context.TutorshipSubjects
            .Include(s => s.Tutorship)
                .ThenInclude(t => t.SystemUser)
            .Include(s => s.TutorshipSubjectLanguages)
                .ThenInclude(tsl => tsl.Language)
            .Include(s => s.Reviews) // Needed to populate the AverageRating [NotMapped] property
            .Select(s => new SubjectExploreDto
            {
                SubjectId = s.Id,
                SubjectName = s.SubjectName,
                Outline = s.Outline,
                Availability = s.Availability,
                Level = s.Level,
                HourlyRate = s.HourlyRate,

                TutorName = s.Tutorship.SystemUser.FirstName + " " + s.Tutorship.SystemUser.LastName,
                TutorEmail = s.Tutorship.SystemUser.Email,
                ProfilePhotoPath = s.Tutorship.ProfilePhotoPath, // Will be replaced below
                LanguageNames = s.TutorshipSubjectLanguages
                                .Select(tsl => tsl.Language.Name)
                                .ToList(),

                AverageRating = s.AverageRating
            })
            .ToListAsync();

        // Convert image path to Base64 string
        foreach (var subject in subjects)
        {
            if (!string.IsNullOrEmpty(subject.ProfilePhotoPath))
            {
                try
                {

                    subject.ProfilePhotoPath = ConvertImageToBase64(Path.Combine(_environment.WebRootPath, subject.ProfilePhotoPath));
                }
                catch (Exception ex)
                {
                    // Log or handle image conversion failure — for now we just skip it
                    subject.ProfilePhotoPath = null;
                }
            }
        }

        return subjects;
    }

}
