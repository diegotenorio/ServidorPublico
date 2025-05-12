using System.Threading;
using System.Threading.Tasks;
using ServidorPublico.Application.Commands;
using ServidorPublico.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ServidorPublico.Tests
{
    public class CreateServidorHandlerTests
    {
        [Fact]
        public async Task DeveCriarServidorComSucesso()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var context = new AppDbContext(options);
            var handler = new CreateServidorHandler(context);

            var command = new CreateServidorCommand
            {
                Nome = "Teste",
                Telefone = "1111-1111",
                Email = "teste@email.com",
                OrgaoId = 1,
                LotacaoId = 1,
                Sala = "A101"
            };

            var id = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(default, id);
        }
    }
}
