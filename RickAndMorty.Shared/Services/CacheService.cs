using Microsoft.Extensions.Caching.Memory;

using Newtonsoft.Json;

using RickAndMorty.Shared.Services.Abstractions;


namespace RickAndMorty.Shared.Services;
public class CacheService : ICacheService
{
    private readonly IMemoryCache cache;
    public CacheService(IMemoryCache cache)
    {
        this.cache = cache;
    }

    public T SetObjectInCache<T>(string cacheKey, int slidingExpirationInSeconds, int absoluteExpirationInMinutes, T value) where T : class
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpirationInSeconds))
                .SetAbsoluteExpiration(TimeSpan.FromHours(absoluteExpirationInMinutes));

        var cachedData = JsonConvert.SerializeObject(value);
        
        cache.Set(cacheKey, cachedData, cacheEntryOptions); 

        return value;
    }

    public T? GetObjectFromCache<T>(string cacheKey) where T : class
    {
        if (cache.TryGetValue(cacheKey, out string cachedData))
        {
            return JsonConvert.DeserializeObject<T>(cachedData);
        }

        return null;
    }
}