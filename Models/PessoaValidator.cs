using FluentValidation;
using System.Text.RegularExpressions;

namespace TrilhaApiDesafio.Models
{
    public sealed class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator()
        {
            RuleFor(_ => _.Id)
                .Empty().WithMessage("O ID deve ser passado nulo");

            RuleFor(_ => _.Nome)
                .NotEmpty().WithMessage("Informe o nome da pessoa")
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres");

            RuleFor(_ => _.CPF)
                .Length(11).WithMessage("CPF precisa ter 11 dígitos, sem espaços, pontos ou traços")
                .Matches(new Regex(@"([0-9]{11})")).WithMessage("CPF inválido");

            RuleFor(_ => _.DataNascimento)
                .NotEmpty().WithMessage("Informe a data de nascimento")
                .GreaterThan(DateTime.MinValue).WithMessage("Data deve ser válida")
                .LessThan(DateTime.Now.Date).WithMessage("Data deve ser anterior ao dia de hoje");
        }
    }
}