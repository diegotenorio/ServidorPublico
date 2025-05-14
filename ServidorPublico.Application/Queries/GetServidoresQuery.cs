using System.Collections.Generic;
using MediatR;
using ServidorPublico.Application.DTOs;

namespace ServidorPublico.Application.Queries
{
    /// <summary>
    /// Representa uma consulta para buscar servidores públicos com possíveis filtros por nome, órgão e lotação.
    /// Utiliza o padrão CQRS com MediatR para separação clara da lógica de leitura.
    /// </summary>
    public class GetServidoresQuery : IRequest<List<ServidorDto>>
    {
        /// <summary>
        /// Filtro opcional pelo nome do servidor.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Filtro opcional pelo identificador do órgão.
        /// </summary>
        public int? OrgaoId { get; set; }

        /// <summary>
        /// Filtro opcional pelo identificador da lotação.
        /// </summary>
        public int? LotacaoId { get; set; }
    }
}
