using InhlwathiTutors.Models;
using InhlwathiTutors.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InhlwathiTutors.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<SystemUser> _userManager;
        private readonly SignInManager<SystemUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<SystemUser> userManager,
            SignInManager<SystemUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterAsync(SystemUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }

        // ✅ Register and return JWT token
        public async Task<(IdentityResult Result, string Token)> RegisterWithTokenAsync(SystemUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return (result, null);

            var token = await GenerateJwtTokenAsync(user);
            return (result, token);
        }

        // ✅ Login and return JWT token
        public async Task<string?> LoginAndGenerateTokenAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
                
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                return null;

            return await GenerateJwtTokenAsync(user);
        }

        // 🔒 Private helper to generate JWT
        private async Task<string> GenerateJwtTokenAsync(SystemUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
