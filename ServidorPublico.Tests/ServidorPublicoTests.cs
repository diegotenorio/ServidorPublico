using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using ServidorPublico.Application.Commands;
using ServidorPublico.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ServidorPublico.Tests
{
    /// <summary>
    /// Testes unitários para os handlers de comandos: criação, atualização e inativação de servidor.
    /// </summary>
    public class ServidorCommandHandlersTests
    {
        private AppDbContext CriarDbContextEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        /// <summary>
        /// Verifica se o handler de criação salva corretamente um novo servidor e retorna o Id.
        /// </summary>
        [Fact(DisplayName = "Deve criar um novo servidor com sucesso")]
        public async Task CreateServidorHandler_DeveCriarComSucesso()
        {
            // Arrange
            var context = CriarDbContextEmMemoria();
            var handler = new CreateServidorHandler(context);
            var command = new CreateServidorCommand
            {
                Nome = "João Silva",
                Email = "joao@teste.com",
                Telefone = "99999-9999",
                OrgaoId = 1,
                LotacaoId = 2,
                Sala = "101"
            };

            // Act
            var id = await handler.Handle(command, CancellationToken.None);

            // Assert
            var servidorCriado = await context.Servidores.FindAsync(id);
            servidorCriado.Should().NotBeNull();
            servidorCriado!.Nome.Should().Be("João Silva");
        }

        /// <summary>
        /// Verifica se o handler de atualização altera os dados corretamente quando o servidor existe.
        /// </summary>
        [Fact(DisplayName = "Deve atualizar dados de um servidor existente")]
        public async Task UpdateServidorHandler_DeveAtualizarComSucesso()
        {
            // Arrange
            var context = CriarDbContextEmMemoria();
            var servidor = new Domain.Servidor
            {
                Nome = "Original",
                Email = "original@teste.com",
                OrgaoId = 1,
                LotacaoId = 1,
                Sala = "A1"
            };
            context.Servidores.Add(servidor);
            await context.SaveChangesAsync();

            var handler = new UpdateServidorHandler(context);
            var command = new UpdateServidorCommand
            {
                Id = servidor.Id,
                Nome = "Atualizado",
                Email = "novo@teste.com",
                Telefone = "88888-8888",
                OrgaoId = 2,
                LotacaoId = 2,
                Sala = "B2"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            var atualizado = await context.Servidores.FindAsync(servidor.Id);
            atualizado!.Nome.Should().Be("Atualizado");
        }

        /// <summary>
        /// Verifica se o handler de inativação desativa corretamente um servidor ativo.
        /// </summary>
        [Fact(DisplayName = "Deve inativar um servidor ativo com sucesso")]
        public async Task InativarServidorHandler_DeveInativarComSucesso()
        {
            // Arrange
            var context = CriarDbContextEmMemoria();
            var servidor = new Domain.Servidor
            {
                Nome = "Ativo",
                Email = "ativo@teste.com",
                OrgaoId = 1,
                LotacaoId = 1,
                Sala = "A1",
                Ativo = true
            };
            context.Servidores.Add(servidor);
            await context.SaveChangesAsync();

            var handler = new InativarServidorHandler(context);
            var command = new InativarServidorCommand { Id = servidor.Id };

            // Act
            var resultado = await handler.Handle(command, CancellationToken.None);

            // Assert
            resultado.Should().BeTrue();
            var atualizado = await context.Servidores.FindAsync(servidor.Id);
            atualizado!.Ativo.Should().BeFalse();
        }

        /// <summary>
        /// Verifica se o handler de atualização retorna false ao tentar alterar um servidor inexistente.
        /// </summary>
        [Fact(DisplayName = "Deve retornar false ao tentar atualizar servidor inexistente")]
        public async Task UpdateServidorHandler_ServidorInexistente_DeveRetornarFalse()
        {
            var context = CriarDbContextEmMemoria();
            var handler = new UpdateServidorHandler(context);

            var command = new UpdateServidorCommand
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Email = "teste@email.com",
                OrgaoId = 1,
                LotacaoId = 1,
                Sala = "101"
            };

            var resultado = await handler.Handle(command, CancellationToken.None);
            resultado.Should().BeFalse();
        }

        /// <summary>
        /// Deve falhar ao criar servidor com nome vazio.
        /// </summary>
        [Fact(DisplayName = "Deve falhar ao criar servidor com nome vazio")]
        public void CreateServidorValidator_DeveFalharSeNomeForVazio()
        {
            var validator = new CreateServidorValidator();

            var command = new CreateServidorCommand
            {
                Nome = "",
                Email = "teste@teste.com",
                OrgaoId = 1,
                LotacaoId = 1,
                Sala = "100"
            };

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        /// <summary>
        /// Deve falhar ao criar servidor se OrgaoId for zero.
        /// </summary>
        [Fact(DisplayName = "Deve falhar ao criar servidor com OrgaoId = 0")]
        public void CreateServidorValidator_DeveFalharSeOrgaoIdForZero()
        {
            var validator = new CreateServidorValidator();

            var command = new CreateServidorCommand
            {
                Nome = "Servidor",
                Email = "teste@teste.com",
                OrgaoId = 0,
                LotacaoId = 1,
                Sala = "100"
            };

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.OrgaoId);
        }

        /// <summary>
        /// Deve falhar ao criar servidor se LotacaoId for zero.
        /// </summary>
        [Fact(DisplayName = "Deve falhar ao criar servidor com LotacaoId = 0")]
        public void CreateServidorValidator_DeveFalharSeLotacaoIdForZero()
        {
            var validator = new CreateServidorValidator();

            var command = new CreateServidorCommand
            {
                Nome = "Servidor",
                Email = "teste@teste.com",
                OrgaoId = 1,
                LotacaoId = 0,
                Sala = "100"
            };

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.LotacaoId);
        }
    }
}
