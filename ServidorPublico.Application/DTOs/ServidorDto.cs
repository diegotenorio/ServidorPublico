using System;

namespace ServidorPublico.Application.DTOs
{
    /// <summary>
    /// Objeto de Transferência de Dados (DTO) usado para expor informações públicas do servidor.
    /// Utilizado em respostas de queries e endpoints REST.
    /// </summary>
    public class ServidorDto
    {
        /// <summary>
        /// Identificador único do servidor.
        /// </summary>
        public Guid Id { get; set; }

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
        /// Identificador do órgão ao qual o servidor está vinculado.
        /// </summary>
        public int OrgaoId { get; set; }

        /// <summary>
        /// Identificador da lotação atual do servidor.
        /// </summary>
        public int LotacaoId { get; set; }

        /// <summary>
        /// Sala física onde o servidor está alocado.
        /// </summary>
        public string Sala { get; set; }
    }
}
