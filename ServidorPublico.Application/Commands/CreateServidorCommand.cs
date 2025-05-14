using System;
using MediatR;
using ServidorPublico.Application.Validators;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Comando responsável por encapsular os dados necessários para cadastrar um novo servidor público.
    /// Utiliza o padrão CQRS com MediatR e implementa <see cref="IServidorCommandBase"/> para reaproveitamento das validações.
    /// </summary>
    public class CreateServidorCommand : IRequest<Guid>, IServidorCommandBase
    {
        /// <summary>
        /// Nome completo do servidor.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Número de telefone para contato do servidor.
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// Endereço de e-mail institucional do servidor.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identificador do órgão ao qual o servidor será vinculado.
        /// </summary>
        public int OrgaoId { get; set; }

        /// <summary>
        /// Identificador da lotação inicial do servidor.
        /// </summary>
        public int LotacaoId { get; set; }

        /// <summary>
        /// Sala física onde o servidor estará alocado.
        /// </summary>
        public string Sala { get; set; }
    }
}
