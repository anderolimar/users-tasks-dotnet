using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;

namespace UsersTasks.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthLogin(Login login);
    }
}
