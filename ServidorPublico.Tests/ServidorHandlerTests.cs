using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ServidorPublico.Application.Commands;
using ServidorPublico.Infrastructure;

namespace ServidorPublico.Tests
{
    public class ServidorHandlerTests
    {
        // Método auxiliar que gera um novo contexto InMemory isolado por teste
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Garante banco limpo por teste
                .Options;

            return new AppDbContext(options);
        }

        [Fact(DisplayName = "Deve criar servidor com sucesso quando os dados são válidos")]
        public async Task DeveCriarServidorComSucesso()
        {
            var context = GetInMemoryContext();
            var handler = new CreateServidorHandler(context);

            // Comando válido
            var command = new CreateServidorCommand
            {
                Nome = "Servidor de Teste",
                Telefone = "61999999999",
                Email = "servidor@email.com",
                OrgaoId = 1,
                LotacaoId = 2,
                Sala = "101"
            };

            // Execução do handler
            var id = await handler.Handle(command, CancellationToken.None);

            // Validação: o ID gerado não deve ser default (Guid vazio)
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact(DisplayName = "Deve falhar ao criar servidor com nome vazio")]
        public async Task DeveFalharSeNomeForVazio()
        {
            var context = GetInMemoryContext();
            var handler = new CreateServidorHandler(context);

            var command = new CreateServidorCommand
            {
                Nome = "", // Inválido
                Telefone = "61999999999",
                Email = "semnome@email.com",
                OrgaoId = 1,
                LotacaoId = 1,
                Sala = "102"
            };

            await Assert.ThrowsAsync<DbUpdateException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Deve falhar se OrgaoId for zero")]
        public async Task DeveFalharSeOrgaoIdForZero()
        {
            var context = GetInMemoryContext();
            var handler = new CreateServidorHandler(context);

            var command = new CreateServidorCommand
            {
                Nome = "Servidor Sem Órgão",
                Telefone = "61999999999",
                Email = "semorgao@email.com",
                OrgaoId = 1, // Inválido
                LotacaoId = 2,
                Sala = "103"
            };

            await Assert.ThrowsAsync<DbUpdateException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Deve falhar se LotacaoId for zero")]
        public async Task DeveFalharSeLotacaoIdForZero()
        {
            var context = GetInMemoryContext();
            var handler = new CreateServidorHandler(context);

            var command = new CreateServidorCommand
            {
                Nome = "Servidor Sem Lotação",
                Telefone = "61999999999",
                Email = "semlotacao@email.com",
                OrgaoId = 1,
                LotacaoId = 1, // Inválido
                Sala = "104"
            };

            await Assert.ThrowsAsync<DbUpdateException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }
    }
}
