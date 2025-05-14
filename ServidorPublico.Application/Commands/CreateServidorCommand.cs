using System;
using MediatR;
using ServidorPublico.Application.Validators;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Comando respons�vel por encapsular os dados necess�rios para cadastrar um novo servidor p�blico.
    /// Utiliza o padr�o CQRS com MediatR e implementa <see cref="IServidorCommandBase"/> para reaproveitamento das valida��es.
    /// </summary>
    public class CreateServidorCommand : IRequest<Guid>, IServidorCommandBase
    {
        /// <summary>
        /// Nome completo do servidor.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// N�mero de telefone para contato do servidor.
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// Endere�o de e-mail institucional do servidor.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identificador do �rg�o ao qual o servidor ser� vinculado.
        /// </summary>
        public int OrgaoId { get; set; }

        /// <summary>
        /// Identificador da lota��o inicial do servidor.
        /// </summary>
        public int LotacaoId { get; set; }

        /// <summary>
        /// Sala f�sica onde o servidor estar� alocado.
        /// </summary>
        public string Sala { get; set; }
    }
}
