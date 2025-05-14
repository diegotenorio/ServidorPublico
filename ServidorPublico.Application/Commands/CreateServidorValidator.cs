using ServidorPublico.Application.Commands;
using ServidorPublico.Application.Validators;

/// <summary>
/// Validador responsável por aplicar as regras de negócio ao comando <see cref="CreateServidorCommand"/>.
/// Herda as validações genéricas da base <see cref="ServidorValidatorBase{T}"/>.
/// </summary>
public class CreateServidorValidator : ServidorValidatorBase<CreateServidorCommand>
{
    /// <summary>
    /// Construtor padrão que herda todas as regras de validação comuns.
    /// Nenhuma validação adicional específica para criação é necessária neste momento.
    /// </summary>
    public CreateServidorValidator()
    {
        // Validações estão todas na classe base
    }
}
