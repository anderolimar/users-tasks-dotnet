using Microsoft.Extensions.Caching.Hybrid;
using UsersTasks.Interfaces;

namespace UsersTasks.Services
{ 
    public class CacheService : ICacheService
    {
        private readonly HybridCache cache;

        public CacheService(HybridCache cache)
        {
            this.cache = cache;
        }

        public async Task<T> GetOrAddCacheDataAsync<T>(string key, Func<CancellationToken, Task<T>> factory)
        {
            var res = await this.cache.GetOrCreateAsync(key, async (e) => {
                var value = await factory(e);
                return value;
             });
            return res;
        }
    }
}
