using System;

namespace ServidorPublico.Domain
{
    /// <summary>
    /// Representa um servidor público com dados cadastrais e de alocação.
    /// </summary>
    public class Servidor
    {
        /// <summary>
        /// Identificador único do servidor (GUID gerado automaticamente).
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nome completo do servidor.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Número de telefone para contato.
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// Endereço de e-mail institucional do servidor.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identificador do órgão ao qual o servidor está vinculado.
        /// </summary>
        public int OrgaoId { get; set; }

        /// <summary>
        /// Identificador da lotação atual do servidor.
        /// </summary>
        public int LotacaoId { get; set; }

        /// <summary>
        /// Número ou código da sala física onde o servidor está alocado.
        /// </summary>
        public string Sala { get; set; }

        /// <summary>
        /// Indica se o servidor está ativo no sistema.
        /// </summary>
        public bool Ativo { get; set; } = true;
    }
}
