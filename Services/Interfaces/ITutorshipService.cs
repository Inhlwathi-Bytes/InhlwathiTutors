using InhlwathiTutors.Models;

public interface ITutorshipService
{
    Task<Tutorship> CreateTutorshipAsync(string userId, CreateTutorshipDto dto);
    Task<string> GetUserModeAsync(string userId);
    Task<bool> ChangeUserModeAsync(string userId, string newMode);
    Task<TutorshipDto?> GetProfileAsync(string userId);
}