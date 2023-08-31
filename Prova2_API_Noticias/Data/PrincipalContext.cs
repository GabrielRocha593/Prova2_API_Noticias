using Microsoft.EntityFrameworkCore;
using Prova2_API_Noticias.Models;

namespace Prova2_API_Noticias.Data
{
    public class PrincipalContext : DbContext
    {
        public PrincipalContext(DbContextOptions<PrincipalContext> opts) : base(opts)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

        public DbSet<Noticia> Noticia { get; set; }

    }
}
