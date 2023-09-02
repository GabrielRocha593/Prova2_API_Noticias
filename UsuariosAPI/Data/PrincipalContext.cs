using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Models;

namespace UsuariosAPI.Data;

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
