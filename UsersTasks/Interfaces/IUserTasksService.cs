using System.Threading.Tasks;
using UsersTasks.Models;
using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;

namespace UsersTasks.Interfaces
{
    public interface IUserTasksService
    {
        Task<UserTasksResponse> GetUserTasks(int userId, int page, int pagesize);
        Task<NewUserTaskResponse> AddUserTask(int userId, NewUserTask newUserTask);
    }
}

