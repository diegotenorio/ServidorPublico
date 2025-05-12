using MediatR;
using ServidorPublico.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorPublico.Application.Commands
{
    public class InativarServidorHandler : IRequestHandler<InativarServidorCommand, bool>
    {
        private readonly AppDbContext _context;

        public InativarServidorHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(InativarServidorCommand request, CancellationToken cancellationToken)
        {
            var servidor = await _context.Servidores.FindAsync(request.Id);
            if (servidor == null || !servidor.Ativo)
                return false;

            servidor.Ativo = false;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
