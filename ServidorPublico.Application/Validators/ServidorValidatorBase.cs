using FluentValidation;

namespace ServidorPublico.Application.Validators
{
    // Classe base genérica que aplica regras comuns de validação a qualquer tipo T
    public abstract class ServidorValidatorBase<T> : AbstractValidator<T>
        where T : class, IServidorCommandBase
    {
        protected ServidorValidatorBase()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)                
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            RuleFor(x => x.OrgaoId)
                .GreaterThan(0).WithMessage("O OrgaoId deve ser maior que zero.");

            RuleFor(x => x.LotacaoId)
                .GreaterThan(0).WithMessage("O LotacaoId deve ser maior que zero.");

            RuleFor(x => x.Sala)
                .MaximumLength(10).WithMessage("A sala deve ter no máximo 10 caracteres.");
        }
    }

    // Interface base que os comandos devem implementar para serem compatíveis
    public interface IServidorCommandBase
    {
        string Nome { get; }
        string Email { get; }
        int OrgaoId { get; }
        int LotacaoId { get; }
        string Sala { get; }
    }
}
