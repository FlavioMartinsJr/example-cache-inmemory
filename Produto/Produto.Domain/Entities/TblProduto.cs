using System.ComponentModel.DataAnnotations;

namespace Produto.Domain.Entities
{
    public class TblProduto
    {
        [Key]
        public virtual int? Id { get; set; }
        public virtual string? Titulo { get; set; }
        public virtual string? Descricao { get; set; }
        public virtual Decimal? Valor { get; set; }
        public virtual DateTime? DataCriado { get; set; }
        public virtual DateTime? DataAlterado { get; set; }
    }  
}
