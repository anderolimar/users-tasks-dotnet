namespace UsersTasks.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetOrAddCacheDataAsync<T>(string key, Func<CancellationToken, Task<T>> factory);
    }
}

