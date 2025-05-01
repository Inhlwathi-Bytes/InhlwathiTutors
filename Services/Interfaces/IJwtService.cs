using System.Security.Claims;

namespace InhlwathiTutors.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
        ClaimsPrincipal? ValidateToken(string token);
    }

}
