using MediatR;
using ServidorPublico.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Handler responsável por processar o comando <see cref="InativarServidorCommand"/>,
    /// realizando a inativação lógica (soft delete) de um servidor no sistema.
    /// </summary>
    public class InativarServidorHandler : IRequestHandler<InativarServidorCommand, bool>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que injeta o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Instância do <see cref="AppDbContext"/> usada para persistência.</param>
        public InativarServidorHandler(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Executa a lógica para inativar um servidor, realizando uma exclusão lógica.
        /// </summary>
        /// <param name="request">Comando contendo o identificador do servidor a ser inativado.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>
        /// Retorna <c>true</c> se a operação foi bem-sucedida, ou <c>false</c> se o servidor não existir ou já estiver inativo.
        /// </returns>
        public async Task<bool> Handle(InativarServidorCommand request, CancellationToken cancellationToken)
        {
            var servidor = await _context.Servidores.FindAsync(request.Id);

            // Se o servidor não existe ou já está inativo, retorna false
            if (servidor == null || !servidor.Ativo)
                return false;

            // Marca o servidor como inativo (exclusão lógica)
            servidor.Ativo = false;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

