using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServidorPublico.Domain;
using ServidorPublico.Infrastructure;

namespace ServidorPublico.Application.Commands
{
    public class CreateServidorHandler : IRequestHandler<CreateServidorCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateServidorHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateServidorCommand request, CancellationToken cancellationToken)
        {
            var servidor = new Servidor
            {
                Nome = request.Nome,
                Telefone = request.Telefone,
                Email = request.Email,
                OrgaoId = request.OrgaoId,
                LotacaoId = request.LotacaoId,
                Sala = request.Sala
            };

            _context.Servidores.Add(servidor);
            await _context.SaveChangesAsync();
            return servidor.Id;
        }
    }
}
