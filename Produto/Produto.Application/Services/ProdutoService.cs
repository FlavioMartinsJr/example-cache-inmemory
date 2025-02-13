using AutoMapper;
using Produto.Application.DTOs;
using Produto.Application.Exceptions;
using Produto.Application.Interfaces;
using Produto.Domain.Entities;
using Produto.Domain.Enums;
using Produto.Domain.Interfaces;
using Produto.Domain.Pagination;

namespace Produto.Application.Services
{
    public class ProdutoService(IUnitOfWork unitOfWork, IMapper mapper) : IProdutoService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedList<ProdutoGet>> GetProduto(int pageNumber, int pageSize)
        {
            PagedList<TblProduto> produto = await _unitOfWork.ProdutoRepository.ToPagination(pageNumber, pageSize);
            IEnumerable<ProdutoGet> produtoGet = _mapper.Map<IEnumerable<ProdutoGet>>(produto.Dados);
            return new PagedList<ProdutoGet>(produtoGet, pageNumber, pageSize, produto.TotalCount);
        }

        public async Task<ProdutoGet> GetProdutoById(int id)
        {
            TblProduto? produtoGet = await _unitOfWork.ProdutoRepository.FirstOrDefaultAsync(filter: x => x.Id == id);
            return _mapper.Map<ProdutoGet>(produtoGet);
        }

        public async Task<ProdutoDTO> PostProduto(ProdutoPost request, CancellationToken token)
        {
            if (await _unitOfWork.ProdutoRepository.AnyAsync(x => x.Titulo == request.Titulo))
                throw new UserMessageException(ErrorCode.ItemAlreadyExists, "Falha no PostProduto - item_already_exists", "Já existe um registro com esse titulo");

            TblProduto produtoPost = _mapper.Map<TblProduto>(request);
            await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.ProdutoRepository.AddAsync(produtoPost), token);
            return _mapper.Map<ProdutoDTO>(produtoPost);
        }

        public async Task<ProdutoDTO> PutProdutoById(int id, ProdutoPut request, CancellationToken token)
        {
            TblProduto existProduto = await _unitOfWork.ProdutoRepository.FirstOrDefaultAsync(filter: x => x.Id == id)
            ?? throw new UserMessageException(ErrorCode.NotFound, "Falha no PutProdutoById id não foi encontrado - not_found", "Produto não foi encontrado");

            request.Id = id;
            request.Titulo = request.Titulo ?? existProduto.Titulo;
            request.Valor = request.Valor ?? existProduto.Valor;
            request.DataCriado = existProduto.DataCriado;

            TblProduto produtoPut = _mapper.Map<TblProduto>(request);
            await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.ProdutoRepository.Update(produtoPut), token);
            return _mapper.Map<ProdutoDTO>(produtoPut);
        }
    }
}
