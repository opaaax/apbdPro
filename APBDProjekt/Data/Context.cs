using APBDProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Data;

public class Context : DbContext
{
    public DbSet<Firma> Firmy { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<OsobaFizyczna> OsobaFizyczne { get; set; }
    
    protected Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}