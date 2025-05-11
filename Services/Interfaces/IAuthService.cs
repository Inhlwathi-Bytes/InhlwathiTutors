using InhlwathiTutors.Models;
using Microsoft.AspNetCore.Identity;

namespace InhlwathiTutors.Services.Interfaces
{
    public interface IAuthService
    {
            Task<IdentityResult> RegisterAsync(SystemUser user, string password);
            Task<SignInResult> LoginAsync(string email, string password);

        // New overloads that return token too (optional to use)
        Task<(IdentityResult Result, string Token)> RegisterWithTokenAsync(SystemUser user, string password);
        Task<string?> LoginAndGenerateTokenAsync(string email, string password);
    }
}
