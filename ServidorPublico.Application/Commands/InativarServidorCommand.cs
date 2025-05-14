using MediatR;
using System;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Comando respons�vel por solicitar a inativa��o (exclus�o l�gica) de um servidor p�blico.
    /// Utilizado no padr�o CQRS com MediatR, retornando um <c>bool</c> que indica sucesso ou falha da opera��o.
    /// </summary>
    public class InativarServidorCommand : IRequest<bool>
    {
        /// <summary>
        /// Identificador �nico do servidor a ser inativado.
        /// </summary>
        public Guid Id { get; set; }
    }
}
