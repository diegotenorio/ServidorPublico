using System.Collections.Generic;
using MediatR;
using ServidorPublico.Application.DTOs;

namespace ServidorPublico.Application.Queries
{
    /// <summary>
    /// Representa uma consulta para buscar servidores p�blicos com poss�veis filtros por nome, �rg�o e lota��o.
    /// Utiliza o padr�o CQRS com MediatR para separa��o clara da l�gica de leitura.
    /// </summary>
    public class GetServidoresQuery : IRequest<List<ServidorDto>>
    {
        /// <summary>
        /// Filtro opcional pelo nome do servidor.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Filtro opcional pelo identificador do �rg�o.
        /// </summary>
        public int? OrgaoId { get; set; }

        /// <summary>
        /// Filtro opcional pelo identificador da lota��o.
        /// </summary>
        public int? LotacaoId { get; set; }
    }
}
