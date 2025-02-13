using Produto.Application.Interfaces;
using Produto.Domain.Pagination;
using Microsoft.Extensions.Caching.Memory;


namespace Produto.Application.Services
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        private readonly IMemoryCache _memoryCache = memoryCache;

        private string GenerateCacheKey(string cacheKeyBase, int pageNumber, int pageSize)
        {
            return $"{cacheKeyBase}_PageNumber{pageNumber}_PageSize{pageSize}";
        }

        private string GenerateCacheKey(string cacheKeyBase, int id)
        {
            return $"{cacheKeyBase}_id{id}";
        }

        public async Task<PagedList<T>> GetCache<T>(string cacheKey, int pageNumber, int pageSize, Func<int, int, Task<PagedList<T>>> fetchData)
        {
            string? cacheKeyBase = GenerateCacheKey(cacheKey, pageNumber, pageSize);

            if (!_memoryCache.TryGetValue(cacheKeyBase, out PagedList<T>? result))
            {
                result = await fetchData(pageNumber, pageSize);
                MemoryCacheEntryOptions? cacheOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromSeconds(120))
               .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
               .SetPriority(CacheItemPriority.Normal);
                _memoryCache.Set(cacheKeyBase, result, cacheOptions);
            }

            return result!;
        }

        public async Task<T> GetCacheById<T>(string cacheKey, int id, Func<int, Task<T>> fetchDataById)
        {
            string? cacheKeyBase = GenerateCacheKey(cacheKey, id);

            if (!_memoryCache.TryGetValue(cacheKeyBase, out T? result))
            {
                result = await fetchDataById(id);
                MemoryCacheEntryOptions? cacheOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromSeconds(120))
               .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
               .SetPriority(CacheItemPriority.Normal);
                _memoryCache.Set(cacheKeyBase, result, cacheOptions);
            }

            return result!;
        }

        public async Task DeleteAllCache()
        {
            if (_memoryCache is MemoryCache cache)
            {
                cache.Compact(1.0); 
            }
            await Task.CompletedTask;
        }

    }
}
