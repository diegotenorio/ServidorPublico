using FluentValidation;
using ServidorPublico.Application.Commands;

public class InativarServidorValidator : AbstractValidator<InativarServidorCommand>
{
    public InativarServidorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O Id do servidor é obrigatório para inativação.");
    }
}