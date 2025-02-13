using Produto.Application.DTOs;
using Produto.Domain.Pagination;
using System.Runtime.CompilerServices;

namespace Produto.Application.Interfaces
{
    public interface ICacheService
    {
        Task<PagedList<T>> GetCache<T>(string cacheKey, int pageNumber, int pageSize, Func<int, int, Task<PagedList<T>>> fetchData);
        Task<T> GetCacheById<T>(string cacheKey, int id, Func<int, Task<T>> fetchDataById);
        Task DeleteAllCache();
    }
}
