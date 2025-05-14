using System;

namespace ServidorPublico.Application.DTOs
{
    /// <summary>
    /// Objeto de Transfer�ncia de Dados (DTO) usado para expor informa��es p�blicas do servidor.
    /// Utilizado em respostas de queries e endpoints REST.
    /// </summary>
    public class ServidorDto
    {
        /// <summary>
        /// Identificador �nico do servidor.
        /// </summary>
        public Guid Id { get; set; }

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
        /// Identificador do �rg�o ao qual o servidor est� vinculado.
        /// </summary>
        public int OrgaoId { get; set; }

        /// <summary>
        /// Identificador da lota��o atual do servidor.
        /// </summary>
        public int LotacaoId { get; set; }

        /// <summary>
        /// Sala f�sica onde o servidor est� alocado.
        /// </summary>
        public string Sala { get; set; }
    }
}
