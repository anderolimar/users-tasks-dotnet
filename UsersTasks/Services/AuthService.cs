using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using UsersTasks.Interfaces;
using UsersTasks.Models.Auth;
using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;

namespace UsersTasks.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthService> _logger;
        private readonly ITokenService _tokenService; 

        public AuthService(UserManager<ApplicationUser> userManager,
        ILogger<AuthService> logger,
        ITokenService tokenService)
        {
            _userManager = userManager;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> AuthLogin(Login login)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(login.Username);
                if (user == null)
                {
                    return new AuthInvalidCredentialReponse();
                }
                bool isValidPassword = await _userManager.CheckPasswordAsync(user, login.Password);
                if (isValidPassword == false)
                {
                    return new AuthInvalidCredentialReponse();
                }

                List<Claim> authClaims = [
                        new (ClaimTypes.Name, user.UserName),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ];

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = await _tokenService.GenerateToken(authClaims, login.Username);
                return new AuthResponse(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AuthResponse(new InternalServerErrorResponse());
            }

        }
    }
}
