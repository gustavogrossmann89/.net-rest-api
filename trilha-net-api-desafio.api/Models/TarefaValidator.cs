using FluentValidation;

namespace TrilhaApiDesafio.Models
{
    public sealed class TarefaValidator : AbstractValidator<Tarefa>
    {
        public TarefaValidator()
        {
            RuleFor(_ => _.Id)
                .Empty().WithMessage("O ID deve ser passado nulo");

            RuleFor(_ => _.Titulo)
                .NotEmpty().WithMessage("Informe o titulo da tarefa")
                .Length(3, 100).WithMessage("Título deve ter entre 3 e 100 caracteres");

            RuleFor(_ => _.Descricao)
                .MaximumLength(500).WithMessage("Descrição deve conter até um máximo de 500 caracteres");

            RuleFor(_ => _.Data)
                .NotEmpty().WithMessage("Informe a data de execução da tarefa")
                .GreaterThan(DateTime.MinValue).WithMessage("Data deve ser válida");

            RuleFor(_ => _.Status)
                .IsInEnum().WithMessage("Informe o status da tarefa (pendente ou finalizado)");
        }
    }
}