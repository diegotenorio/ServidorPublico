using MediatR;
using ServidorPublico.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorPublico.Application.Commands
{
    // Handler responsável por processar o comando de inativação de um servidor
    // Implementa IRequestHandler do MediatR: recebe um InativarServidorCommand e retorna um bool
    public class InativarServidorHandler : IRequestHandler<InativarServidorCommand, bool>
    {
        private readonly AppDbContext _context;

        // Injeção do contexto do Entity Framework para acesso ao banco de dados
        public InativarServidorHandler(AppDbContext context)
        {
            _context = context;
        }

        // Método que executa a lógica quando o comando é disparado
        public async Task<bool> Handle(InativarServidorCommand request, CancellationToken cancellationToken)
        {
            // Busca o servidor pelo ID informado no comando
            var servidor = await _context.Servidores.FindAsync(request.Id);

            // Se o servidor não existe ou já está inativo, retorna false
            if (servidor == null || !servidor.Ativo)
                return false;

            // Marca o servidor como inativo (exclusão lógica)
            servidor.Ativo = false;

            // Persiste a alteração no banco de dados
            await _context.SaveChangesAsync(cancellationToken);

            // Retorna true indicando que a operação foi realizada com sucesso
            return true;
        }
    }
}
