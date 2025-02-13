using Produto.Domain.Entities;
using Produto.Domain.Interfaces;
using Produto.Domain.Pagination;
using Produto.Infrastructure.Data.Contexts;
using Produto.Infrastructure.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Produto.Infrastructure.Data.Repositories
{
    public class ProdutoRepository(ApplicationDbContext context) : GenericRepository<TblProduto>(context), IProdutoRepository
    {
        private readonly ApplicationDbContext _Context = context;

        public async Task<PagedList<TblProduto>> ToPagination(int pageNumber, int pageSize)
        {
            IQueryable<TblProduto>? query = _Context.Produto.AsNoTracking().OrderBy(x => x.Id).AsQueryable();

            return await ToPaginationHelper.ToPagedList(query, pageNumber, pageSize);
        }
    }
}
