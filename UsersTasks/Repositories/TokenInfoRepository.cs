using UsersTasks.Data;
using UsersTasks.Interfaces;
using UsersTasks.Models.Auth;

namespace UsersTasks.Repositories
{
    public class TokenInfoRepository : Repository<TokenInfo>, ITokenInfoRepository
    {
        public TokenInfoRepository(AppDbContext dBContext) : base(dBContext)
        {
        }
    }
}
