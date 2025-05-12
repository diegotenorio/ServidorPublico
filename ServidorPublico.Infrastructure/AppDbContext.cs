using Microsoft.EntityFrameworkCore;
using ServidorPublico.Domain;

namespace ServidorPublico.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Servidor> Servidores { get; set; }
    }
}
