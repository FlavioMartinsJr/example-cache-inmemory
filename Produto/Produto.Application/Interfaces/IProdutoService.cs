using Produto.Application.DTOs;
using Produto.Domain.Pagination;

namespace Produto.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<PagedList<ProdutoGet>> GetProduto(int pageNumber, int pageSize);
        Task<ProdutoGet> GetProdutoById(int id);
        Task<ProdutoDTO> PostProduto(ProdutoPost request, CancellationToken token);
        Task<ProdutoDTO> PutProdutoById(int id, ProdutoPut request, CancellationToken token);
    }
}
