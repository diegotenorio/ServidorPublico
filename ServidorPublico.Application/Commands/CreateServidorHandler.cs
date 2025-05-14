using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServidorPublico.Domain;
using ServidorPublico.Infrastructure;

namespace ServidorPublico.Application.Commands
{
    /// <summary>
    /// Handler respons�vel por processar o comando <see cref="CreateServidorCommand"/>.
    /// Realiza o cadastro de um novo servidor no sistema.
    /// </summary>
    public class CreateServidorHandler : IRequestHandler<CreateServidorCommand, Guid>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que injeta o contexto de acesso ao banco de dados.
        /// </summary>
        /// <param name="context">Inst�ncia do <see cref="AppDbContext"/> usada para persist�ncia.</param>
        public CreateServidorHandler(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Executa a l�gica de cria��o de um novo servidor.
        /// Persiste os dados no banco e retorna o identificador do novo registro.
        /// </summary>
        /// <param name="request">Comando contendo os dados do servidor a ser criado.</param>
        /// <param name="cancellationToken">Token para cancelamento da opera��o ass�ncrona.</param>
        /// <returns>Identificador <see cref="Guid"/> do servidor criado.</returns>
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
            await _context.SaveChangesAsync(cancellationToken);
            return servidor.Id;
        }
    }
}
