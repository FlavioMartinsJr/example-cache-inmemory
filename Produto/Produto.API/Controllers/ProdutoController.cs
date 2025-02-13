using Produto.API.Models;
using Produto.Application.DTOs;
using Produto.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Produto.API.Controllers
{
    [Tags("Produto")]
    public class ProdutoController(IProdutoService produtoService, ICacheService cacheService) : BaseController
    {
        private readonly IProdutoService _produtoService = produtoService;
        private readonly ICacheService _cacheService = cacheService;
        private readonly string _cacheKey = "Produto";

        /*[Authorize]*/
        [HttpGet("/Produto/BuscarProduto")]
        public async Task<IActionResult> BuscarProduto([FromQuery] PagedModels request)
        {
            return Ok(await _cacheService.GetCache<ProdutoGet>(_cacheKey, request.NumeroPagina, request.ItemsPagina, _produtoService.GetProduto));
        }

        /*[Authorize]*/
        [HttpGet("/Produto/BuscarProdutoId/{id}")]
        public async Task<IActionResult> BuscarProdutoId(int id)
        {
            return Ok(await _cacheService.GetCacheById<ProdutoGet>(_cacheKey, id, _produtoService.GetProdutoById));
        }

        /*[Authorize]*/
        [HttpPost("/Produto/IncluirProduto")]
        public async Task<ActionResult> IncluirProduto([FromBody] ProdutoPost Produto, CancellationToken token)
        {
            await _cacheService.DeleteAllCache();
            return Ok(await _produtoService.PostProduto(Produto, token));
        }

        /*[Authorize]*/
        [HttpPut("/Produto/AtualizarProduto/{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoPut Produto, CancellationToken token)
        {
            await _cacheService.DeleteAllCache();
            return Ok(await _produtoService.PutProdutoById(id, Produto, token));

        }
    }
}
