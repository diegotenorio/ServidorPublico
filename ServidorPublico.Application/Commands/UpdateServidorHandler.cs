using MediatR;
using Microsoft.EntityFrameworkCore;
using ServidorPublico.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorPublico.Application.Commands
{
    public class UpdateServidorHandler : IRequestHandler<UpdateServidorCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateServidorHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateServidorCommand request, CancellationToken cancellationToken)
        {
            var servidor = await _context.Servidores.FindAsync(request.Id);
            if (servidor == null || !servidor.Ativo)
                return false;

            servidor.Nome = request.Nome;
            servidor.Telefone = request.Telefone;
            servidor.Email = request.Email;
            servidor.OrgaoId = request.OrgaoId;
            servidor.LotacaoId = request.LotacaoId;
            servidor.Sala = request.Sala;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
