using MediatR;
using System;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Comando responsável por solicitar a inativação (exclusão lógica) de um servidor público.
    /// Utilizado no padrão CQRS com MediatR, retornando um <c>bool</c> que indica sucesso ou falha da operação.
    /// </summary>
    public class InativarServidorCommand : IRequest<bool>
    {
        /// <summary>
        /// Identificador único do servidor a ser inativado.
        /// </summary>
        public Guid Id { get; set; }
    }
}
