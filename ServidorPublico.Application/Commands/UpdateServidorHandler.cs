using MediatR;
using Microsoft.EntityFrameworkCore;
using ServidorPublico.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Handler responsável por processar o comando <see cref="UpdateServidorCommand"/>,
    /// atualizando os dados de um servidor existente no banco de dados.
    /// </summary>
    public class UpdateServidorHandler : IRequestHandler<UpdateServidorCommand, bool>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que injeta o contexto de acesso ao banco de dados.
        /// </summary>
        /// <param name="context">Instância do contexto <see cref="AppDbContext"/> usada para operações com EF Core.</param>
        public UpdateServidorHandler(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Executa a lógica de atualização de um servidor.
        /// Verifica se o servidor existe e está ativo, e aplica as modificações.
        /// </summary>
        /// <param name="request">Comando contendo os dados atualizados do servidor.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>
        /// Retorna <c>true</c> se a atualização for bem-sucedida,
        /// ou <c>false</c> se o servidor não for encontrado ou estiver inativo.
        /// </returns>
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
