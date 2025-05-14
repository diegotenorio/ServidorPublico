using MediatR;
using System;
using ServidorPublico.Application.Validators;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Comando responsável por encapsular os dados necessários para atualizar um servidor público.
    /// Utiliza o padrão CQRS com MediatR e implementa <see cref="IServidorCommandBase"/> para reutilização de validações.
    /// </summary>
    public class UpdateServidorCommand : IRequest<bool>, IServidorCommandBase
    {
        /// <summary>
        /// Identificador único do servidor a ser atualizado.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome atualizado do servidor.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Telefone de contato atualizado do servidor.
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// Endereço de e-mail institucional atualizado.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identificador do novo órgão ao qual o servidor será vinculado.
        /// </summary>
        public int OrgaoId { get; set; }

        /// <summary>
        /// Identificador da nova lotação do servidor.
        /// </summary>
        public int LotacaoId { get; set; }

        /// <summary>
        /// Código ou número da sala onde o servidor estará alocado.
        /// </summary>
        public string Sala { get; set; }
    }
}
