using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServidorPublico.Application.DTOs;
using ServidorPublico.Infrastructure;

namespace ServidorPublico.Application.Queries
{
    public class GetServidoresHandler : IRequestHandler<GetServidoresQuery, List<ServidorDto>>
    {
        private readonly AppDbContext _context;

        public GetServidoresHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServidorDto>> Handle(GetServidoresQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Servidores.AsQueryable().Where(s => s.Ativo);

            if (!string.IsNullOrWhiteSpace(request.Nome))
                query = query.Where(s => s.Nome.Contains(request.Nome));
            if (request.OrgaoId.HasValue)
                query = query.Where(s => s.OrgaoId == request.OrgaoId);
            if (request.LotacaoId.HasValue)
                query = query.Where(s => s.LotacaoId == request.LotacaoId);

            return await query
                .Select(s => new ServidorDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Telefone = s.Telefone,
                    Email = s.Email,
                    OrgaoId = s.OrgaoId,
                    LotacaoId = s.LotacaoId,
                    Sala = s.Sala
                })
                .ToListAsync(cancellationToken);
        }
    }
}
