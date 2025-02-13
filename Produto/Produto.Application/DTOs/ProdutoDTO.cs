using Produto.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Produto.Application.DTOs
{
    public class ProdutoDTO
    {
        public virtual int? Id { get; set; }
        public virtual string? Titulo { get; set; }
        public virtual string? Descricao { get; set; }
        public virtual Decimal? Valor { get; set; }
        public virtual DateTime? DataCriado { get; set; }
        public virtual DateTime? DataAlterado { get; set; }
    }

    public class ProdutoGet : ProdutoDTO
    {
       
    }

    public class ProdutoPost : ProdutoDTO
    {

        [JsonIgnore]
        [IgnoreDataMember]
        public override int? Id { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public override DateTime? DataCriado { get; set; } = DateTime.Now;

        [JsonIgnore]
        [IgnoreDataMember]
        public override DateTime? DataAlterado { get; set; } = DateTime.Now;
    }

    public class ProdutoPut : ProdutoDTO
    {
        [JsonIgnore]
        [IgnoreDataMember]
        public override int? Id { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public override DateTime? DataAlterado { get; set; } = DateTime.Now;
    }
}
