using UsersTasks.Data;
using UsersTasks.Interfaces;
using UsersTasks.Models;

namespace UsersTasks.Repositories
{
    public class UserTasksRepository : Repository<UserTask>, IUserTasksRepository
    {
        public UserTasksRepository(AppDbContext dBContext) : base(dBContext)
        {
        }
    }
}
