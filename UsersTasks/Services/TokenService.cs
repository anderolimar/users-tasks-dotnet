using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsersTasks.Interfaces;
using System.Security.Cryptography;
using UsersTasks.Models.Auth;


namespace UsersTasks.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenInfoRepository _repository;
        public TokenService(IConfiguration configuration, ITokenInfoRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<Token> GenerateToken(IEnumerable<Claim> authClaims, string username)
        {
            var token = GenerateAccessToken(authClaims);
            string refreshToken = GenerateRefreshToken();
            var tokenInfo = await _repository.
                        FirstOrDefault(a => a.Username == username);

            if (tokenInfo == null)
            {
                tokenInfo = new TokenInfo
                {
                    Username = username,
                    RefreshToken = refreshToken,
                    ExpiredAt = DateTime.UtcNow.AddDays(7)
                };
                await _repository.InsertAsync(tokenInfo);
            }
            else
            {
                tokenInfo.RefreshToken = refreshToken;
                tokenInfo.ExpiredAt = DateTime.UtcNow.AddDays(7);
                await _repository.UpdateAsync(tokenInfo);
            }

            return new Token
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials
                              (authSigningKey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
