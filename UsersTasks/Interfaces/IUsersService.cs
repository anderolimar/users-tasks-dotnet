using System.Threading.Tasks;
using UsersTasks.Models;
using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;

namespace UsersTasks.Interfaces
{
    public interface IUsersService
    {
        Task<UserResponse> GetUser(int id);
        Task<UsersResponse> GetUsers(int page, int pagesize);
        Task<NewUserResponse> AddUser(NewUser newUser);
    }
}
