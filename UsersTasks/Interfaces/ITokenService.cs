using System.Security.Claims;
using UsersTasks.Models.Auth;

namespace UsersTasks.Interfaces
{
    public interface ITokenService
    {
        Task<Token> GenerateToken(IEnumerable<Claim> authClaims, string username);
    }
}
