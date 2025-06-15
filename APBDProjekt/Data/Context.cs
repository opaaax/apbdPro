using APBDProjekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APBDProjekt.Data;

public class Context : IdentityDbContext<IdentityUser>
{
    public DbSet<Firma> Firmy { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<OsobaFizyczna> OsobaFizyczne { get; set; }
    public DbSet<Oprogramowanie> Oprogramowania { get; set; }
    public DbSet<Licencja> Licencje { get; set; }
    public DbSet<Umowa> Umowy { get; set; }
    public DbSet<Platnosc> Platnosci { get; set; }
    public DbSet<Znizka> Znizki { get; set; }
    
    protected Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        });

        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });
        });

        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        });

    }
}