using Produto.Domain.Pagination;
using System.Linq.Expressions;

namespace Produto.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        #region ISVERIFY ACTIONS

        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync();

        #endregion

        #region GET ACTIONS

        Task<T?> FirstOrDefaultAsync( Expression<Func<T, bool>> filter,Func<IQueryable<T>, IQueryable<T>>? include = null);

        #endregion

        #region POST ACTIONS

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        #endregion

        #region PUT ACTIONS
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        #endregion

    }
}