using Microsoft.EntityFrameworkCore;
using Produto.Infrastructure.Data.Contexts;
using System.Linq.Expressions;
using Produto.Domain.Interfaces;

namespace Produto.Infrastructure.Data.Repositories
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
    {
        protected DbSet<T> _context = context.Set<T>();

        #region INSERT

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        #endregion

        #region  SELECT

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return filter == null ? await _context.AnyAsync() : await _context.AnyAsync(filter);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return filter == null ? await _context.CountAsync() : await _context.CountAsync(filter);
        }

        public async Task<int> CountAsync()
        {
            return await _context.CountAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter,Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _context.IgnoreQueryFilters().AsNoTracking();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        #endregion

        #region UPDATE
        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
        }

        #endregion

    }
}
