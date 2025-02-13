using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Produto.API.Models
{
    public class PagedModels
    {
        [Range(1, int.MaxValue, ErrorMessage = "O valor maximo da pagina é 2147483647")]
        public virtual int NumeroPagina { get; set; } = 1;

        [Range(1, 50, ErrorMessage = "O máximo de itens por página é 50.")]
        public virtual int ItemsPagina { get; set; } = 50;

    }
}
