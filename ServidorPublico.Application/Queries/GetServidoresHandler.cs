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
    /// <summary>
    /// Handler responsável por processar a query <see cref="GetServidoresQuery"/>,
    /// aplicando filtros opcionais e retornando a lista de servidores públicos ativos.
    /// </summary>
    public class GetServidoresHandler : IRequestHandler<GetServidoresQuery, List<ServidorDto>>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que injeta o contexto do banco de dados da aplicação.
        /// </summary>
        /// <param name="context">Instância do <see cref="AppDbContext"/> usada para acessar os dados.</param>
        public GetServidoresHandler(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Executa o processamento da query, aplicando filtros e projetando os dados em DTOs.
        /// </summary>
        /// <param name="request">Objeto contendo os filtros opcionais de busca.</param>
        /// <param name="cancellationToken">Token de cancelamento para encerrar a operação de forma antecipada.</param>
        /// <returns>Lista de objetos <see cref="ServidorDto"/> representando os servidores encontrados.</returns>
        public async Task<List<ServidorDto>> Handle(GetServidoresQuery request, CancellationToken cancellationToken)
        {
            // Cria uma query inicial apenas com servidores ativos
            var query = _context.Servidores.AsQueryable().Where(s => s.Ativo);

            // Aplica filtros opcionais enviados na query
            if (!string.IsNullOrWhiteSpace(request.Nome))
                query = query.Where(s => s.Nome.Contains(request.Nome));

            if (request.OrgaoId.HasValue)
                query = query.Where(s => s.OrgaoId == request.OrgaoId);

            if (request.LotacaoId.HasValue)
                query = query.Where(s => s.LotacaoId == request.LotacaoId);

            // Projeta diretamente em DTOs e retorna a lista
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
