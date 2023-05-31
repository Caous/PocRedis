namespace PocWebCacheRedis.Infrastructure.Caching;

public interface ICachingService
{
    Task SetAsync(string Key, string value);
    Task<string> GetAsync(string Key);
}
