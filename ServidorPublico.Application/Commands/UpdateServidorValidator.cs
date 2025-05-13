using ServidorPublico.Application.Commands;
using ServidorPublico.Application.Validators;
using FluentValidation;

public class UpdateServidorValidator : ServidorValidatorBase<UpdateServidorCommand>
{
    public UpdateServidorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O Id do servidor é obrigatório.");
    }
}