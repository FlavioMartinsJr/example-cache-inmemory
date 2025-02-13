using Produto.Application.DTOs;
using FluentValidation;

namespace Produto.API.Validations
{
    public class ProdutoPostValidator : AbstractValidator<ProdutoPost>
    {
        public ProdutoPostValidator() 
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O Titulo não pode ser vazia")
                .MaximumLength(150).WithMessage("O Titulo não pode ultrapassar 150 caracteres.")
                .Matches("^[a-z0-9]+$").WithMessage("O Titulo precisa conter apenas caracteres minusculos e numeros");

            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("O Valor não pode ser vazio");
        }
    }

    public class ProdutoPutValidator : AbstractValidator<ProdutoPut>
    {
        public ProdutoPutValidator()
        {
            RuleFor(x => x.Titulo)
                .MaximumLength(150).WithMessage("O Titulo não pode ultrapassar 150 caracteres.")
                .Matches("^[a-z0-9]+$").WithMessage("O Titulo precisa conter apenas caracteres minusculos e numeros");
        }
    }
}
