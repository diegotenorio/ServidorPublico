using System.Collections.Generic;
using MediatR;
using ServidorPublico.Application.DTOs;

namespace ServidorPublico.Application.Queries
{
    public class GetServidoresQuery : IRequest<List<ServidorDto>>
    {
        public string Nome { get; set; }
        public int? OrgaoId { get; set; }
        public int? LotacaoId { get; set; }
    }
}
