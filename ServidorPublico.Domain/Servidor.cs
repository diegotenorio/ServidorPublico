using System;

namespace ServidorPublico.Domain
{
    /// <summary>
    /// Representa um servidor p�blico com dados cadastrais e de aloca��o.
    /// </summary>
    public class Servidor
    {
        /// <summary>
        /// Identificador �nico do servidor (GUID gerado automaticamente).
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nome completo do servidor.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// N�mero de telefone para contato.
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
        /// N�mero ou c�digo da sala f�sica onde o servidor est� alocado.
        /// </summary>
        public string Sala { get; set; }

        /// <summary>
        /// Indica se o servidor est� ativo no sistema.
        /// </summary>
        public bool Ativo { get; set; } = true;
    }
}
