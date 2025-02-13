using Produto.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Produto.Infrastructure.Data.Helpers
{
    public class ToPaginationHelper
    {
        public static async Task<PagedList<T>> ToPagedList<T>(IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            var count = await source.CountAsync();
            var items = await source.ToListAsync();
            if (pageNumber > 0 && pageSize > 0)
            {
                items = await source.Skip((pageNumber - 1) * pageSize).Take((pageSize)).ToListAsync();
            }
            return new PagedList<T>(items, pageNumber, pageSize, count);
        }
    }
}
