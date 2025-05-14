using MediatR;
using System;
using ServidorPublico.Application.Validators;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Comando respons�vel por encapsular os dados necess�rios para atualizar um servidor p�blico.
    /// Utiliza o padr�o CQRS com MediatR e implementa <see cref="IServidorCommandBase"/> para reutiliza��o de valida��es.
    /// </summary>
    public class UpdateServidorCommand : IRequest<bool>, IServidorCommandBase
    {
        /// <summary>
        /// Identificador �nico do servidor a ser atualizado.
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
        /// Endere�o de e-mail institucional atualizado.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identificador do novo �rg�o ao qual o servidor ser� vinculado.
        /// </summary>
        public int OrgaoId { get; set; }

        /// <summary>
        /// Identificador da nova lota��o do servidor.
        /// </summary>
        public int LotacaoId { get; set; }

        /// <summary>
        /// C�digo ou n�mero da sala onde o servidor estar� alocado.
        /// </summary>
        public string Sala { get; set; }
    }
}
