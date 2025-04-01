using Microsoft.AspNetCore.Connections.Features;
using UsersTasks.Interfaces;
using UsersTasks.Models;
using UsersTasks.Models.Business;
using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;
using UsersTasks.Repositories;

namespace UsersTasks.Services
{   
    public class UserTasksService : IUserTasksService
    {
        IUserTasksRepository _repository;
        private readonly ICacheService _cache;
        private readonly ILogger<UsersService> _logger;
        public UserTasksService(IUserTasksRepository repository, ICacheService cache, ILogger<UsersService> logger)
        {
            _repository = repository;
            _cache = cache;
            _logger = logger;
        }

        public async Task<UserTasksResponse> GetUserTasks(int userId, int page, int pagesize)
        {
            try
            {
                var total = await _cache.GetOrAddCacheDataAsync(
                    $"GetUserTasks_Count_{userId}_{page}_{pagesize}", _ => _repository.CountAsync());
                
                var items = await _cache.GetOrAddCacheDataAsync(
                    $"GetUserTasks_List_{userId}_{page}_{pagesize}", _ => _repository
                        .WhereAsync(i => i.UserId == userId, ((page - 1) * pagesize), pagesize));

                return new UserTasksResponse(items, page, pagesize, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new UserTasksResponse(new InternalServerErrorResponse());
            }
        }

        public async Task<NewUserTaskResponse> AddUserTask(int userId, NewUserTask newUserTask)
        {
            try
            {
                var createdUserTask = await _repository.InsertAsync(new UserTask
                {
                    Title = newUserTask.Title,
                    Description = newUserTask.Description,
                    Status = newUserTask.Status,
                    UserId = userId
                });
                return new NewUserTaskResponse(createdUserTask);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return new NewUserTaskResponse(new InternalServerErrorResponse());
            }
        }
    }
}
