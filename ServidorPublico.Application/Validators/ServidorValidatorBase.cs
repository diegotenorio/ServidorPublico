using FluentValidation;

namespace ServidorPublico.Application.Validators
{
    /// <summary>
    /// Classe base genérica para validação de comandos relacionados a servidores públicos.
    /// Aplica regras comuns de validação para propriedades compartilhadas entre comandos (ex: Create, Update).
    /// </summary>
    /// <typeparam name="T">Tipo do comando que implementa <see cref="IServidorCommandBase"/>.</typeparam>
    public abstract class ServidorValidatorBase<T> : AbstractValidator<T>
        where T : class, IServidorCommandBase
    {
        /// <summary>
        /// Construtor padrão que define as regras de validação compartilhadas entre comandos de servidor.
        /// </summary>
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

    /// <summary>
    /// Interface que define os campos mínimos necessários para comandos de servidor público.
    /// Utilizada para garantir que os validadores genéricos funcionem corretamente com múltiplos comandos.
    /// </summary>
    public interface IServidorCommandBase
    {
        /// <summary>
        /// Nome do servidor.
        /// </summary>
        string Nome { get; }

        /// <summary>
        /// Endereço de e-mail institucional.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Identificador do órgão ao qual o servidor está vinculado.
        /// </summary>
        int OrgaoId { get; }

        /// <summary>
        /// Identificador da lotação atual do servidor.
        /// </summary>
        int LotacaoId { get; }

        /// <summary>
        /// Sala onde o servidor está alocado.
        /// </summary>
        string Sala { get; }
    }
}
