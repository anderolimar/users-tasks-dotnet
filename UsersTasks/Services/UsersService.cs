using Microsoft.EntityFrameworkCore;
using UsersTasks.Interfaces;
using UsersTasks.Models;
using UsersTasks.Models.Dto;
using UsersTasks.Models.Responses;

namespace UsersTasks.Services
{   
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly ILogger<UsersService> _logger;
        private readonly ICacheService _cache;

        public UsersService(IUsersRepository repository, ICacheService cache, ILogger<UsersService> _logger)  {
            _repository = repository;
            _cache = cache;
        }

        public async Task<UserResponse> GetUser(int id) {
            try
            {
                var user = await _cache.GetOrAddCacheDataAsync($"GetUser_{id}", _ => _repository.FindByIdAsync(id));
                if (user == null)
                {
                    return new UserNotFoundReponse();
                }
                return new UserResponse(user);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return new UserResponse(new InternalServerErrorResponse());
            }
        }

        public async Task<UsersResponse> GetUsers(int page, int pagesize)
        {
            try
            {
                var total = await _cache.GetOrAddCacheDataAsync($"GetUsers_Count_{page}_{pagesize}", _ => _repository.CountAsync());
                var items = await _cache.GetOrAddCacheDataAsync(
                    $"GetUser_List_{page}_{pagesize}", _ => _repository.GetAllAsync(((page - 1) * pagesize), pagesize));
                return new UsersResponse(items, page, pagesize, total);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return new UsersResponse(new InternalServerErrorResponse());
            }
        }

        public async Task<NewUserResponse> AddUser(NewUser newUser)
        {
            try
            {
                var user = await _repository.FirstOrDefault(u => u.Email == newUser.Email);
                if (user != null) {
                    return new UserAlreadyExistsReponse();
                }
                var userCreated = await _repository.InsertAsync(
                    new User() { Email = newUser.Email, Name = newUser.Name });
                return new NewUserResponse(userCreated);
            }
            catch (DbUpdateException ex) {
                _logger.LogError(ex.Message);
                return new UserAlreadyExistsReponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new NewUserResponse(new InternalServerErrorResponse());
            }
        }
    }
}
