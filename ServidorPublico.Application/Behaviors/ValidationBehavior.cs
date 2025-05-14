using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorPublico.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior genérico que intercepta qualquer requisição enviada via MediatR e aplica validações usando FluentValidation.
    /// Caso alguma validação falhe, uma exceção <see cref="ValidationException"/> será lançada antes de executar o handler.
    /// </summary>
    /// <typeparam name="TRequest">Tipo da requisição (comando ou query).</typeparam>
    /// <typeparam name="TResponse">Tipo da resposta esperada pelo handler.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Construtor que injeta todos os validadores registrados para o tipo de requisição <typeparamref name="TRequest"/>.
        /// </summary>
        /// <param name="validators">Coleção de validadores aplicáveis à requisição.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Executa o pipeline de validação antes de passar a requisição para o handler correspondente.
        /// </summary>
        /// <param name="request">Requisição recebida (ex: comando ou query).</param>
        /// <param name="next">Delegate que invoca o próximo estágio no pipeline (geralmente o handler).</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Retorna a resposta do tipo <typeparamref name="TResponse"/> se não houver falhas de validação.</returns>
        /// <exception cref="ValidationException">Lançada quando uma ou mais regras de validação são violadas.</exception>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
