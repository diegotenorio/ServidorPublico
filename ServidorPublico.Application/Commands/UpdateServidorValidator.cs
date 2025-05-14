using ServidorPublico.Application.Commands;
using ServidorPublico.Application.Validators;
using FluentValidation;

/// <summary>
/// Validador específico para o comando <see cref="UpdateServidorCommand"/>.
/// Herda as regras genéricas da base <see cref="ServidorValidatorBase{T}"/> e adiciona validações específicas para atualizações.
/// </summary>
public class UpdateServidorValidator : ServidorValidatorBase<UpdateServidorCommand>
{
    /// <summary>
    /// Construtor que define as regras específicas para atualização de servidores.
    /// </summary>
    public UpdateServidorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O Id do servidor é obrigatório.");
    }
}
