using AutoMapper;
using Produto.Application.DTOs;
using Produto.Domain.Entities;

namespace Produto.Application.Mappings
{
    public class DomainDTOMappingProfile : Profile
    {
        public DomainDTOMappingProfile()
        {
        

            CreateMap<TblProduto, ProdutoDTO>().ReverseMap();
            CreateMap<TblProduto, ProdutoPost>().ReverseMap();
            CreateMap<TblProduto, ProdutoPut>().ReverseMap();
            CreateMap<TblProduto, ProdutoGet>().ReverseMap();

        }
    }
}
