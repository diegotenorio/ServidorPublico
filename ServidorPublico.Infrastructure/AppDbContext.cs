using Microsoft.EntityFrameworkCore;
using ServidorPublico.Domain;

namespace ServidorPublico.Infrastructure
{
    /// <summary>
    /// Representa o contexto de banco de dados da aplica��o.
    /// Respons�vel por mapear as entidades para as tabelas do banco e gerenciar as opera��es com EF Core.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Construtor que recebe as op��es de configura��o do DbContext.
        /// Utilizado principalmente para inje��o de depend�ncia.
        /// </summary>
        /// <param name="options">Op��es de configura��o do contexto.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Tabela de servidores p�blicos.
        /// Representa a entidade <see cref="Servidor"/> no banco de dados.
        /// </summary>
        public DbSet<Servidor> Servidores { get; set; }
    }
}
