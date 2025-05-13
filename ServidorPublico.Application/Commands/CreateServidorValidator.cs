using ServidorPublico.Application.Commands;
using ServidorPublico.Application.Validators;

public class CreateServidorValidator : ServidorValidatorBase<CreateServidorCommand>
{
    public CreateServidorValidator()
    {
        // Todas as regras já vêm da base, nada mais a adicionar
    }
}