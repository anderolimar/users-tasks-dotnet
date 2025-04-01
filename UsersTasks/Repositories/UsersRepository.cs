using UsersTasks.Data;
using UsersTasks.Interfaces;
using UsersTasks.Models;

namespace UsersTasks.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(AppDbContext dBContext) : base(dBContext)
        {
           
        }
    }
}
