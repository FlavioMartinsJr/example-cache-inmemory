using Produto.Domain.Entities;
using Produto.Domain.Pagination;

namespace Produto.Domain.Interfaces
{
    public interface IProdutoRepository : IGenericRepository<TblProduto>
    {
        Task<PagedList<TblProduto>> ToPagination(int pageNumber, int pageSize);

    }
}
