using InhlwathiTutors.Models;

public interface ITutorshipService
{
    Task<Tutorship> CreateTutorshipAsync(string userId, CreateTutorshipDto dto);
    Task<string> GetUserModeAsync(string userId);
    Task<bool> ChangeUserModeAsync(string userId, string newMode);
    Task<TutorshipDto?> GetProfileAsync(string userId);
    Task<TutorshipSubject> CreateTutorshipSubjectAsync(CreateTutorshipSubjectDto dto, string userId);
    Task<List<TutorshipSubjectDto>> GetSubjectsAsync(int? optionalTutorshipId, string userId);
}