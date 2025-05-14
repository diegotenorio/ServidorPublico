using FluentValidation;
using ServidorPublico.Application.Commands;

/// <summary>
/// Validador responsável por aplicar regras ao comando <see cref="InativarServidorCommand"/>.
/// Garante que o identificador do servidor a ser inativado seja fornecido.
/// </summary>
public class InativarServidorValidator : AbstractValidator<InativarServidorCommand>
{
    /// <summary>
    /// Construtor que define as regras de validação para inativação de servidor.
    /// </summary>
    public InativarServidorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O Id do servidor é obrigatório para inativação.");
    }
}
