using Microsoft.EntityFrameworkCore;
using ServidorPublico.Domain;

namespace ServidorPublico.Infrastructure
{
    /// <summary>
    /// Representa o contexto de banco de dados da aplicação.
    /// Responsável por mapear as entidades para as tabelas do banco e gerenciar as operações com EF Core.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Construtor que recebe as opções de configuração do DbContext.
        /// Utilizado principalmente para injeção de dependência.
        /// </summary>
        /// <param name="options">Opções de configuração do contexto.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Tabela de servidores públicos.
        /// Representa a entidade <see cref="Servidor"/> no banco de dados.
        /// </summary>
        public DbSet<Servidor> Servidores { get; set; }
    }
}
