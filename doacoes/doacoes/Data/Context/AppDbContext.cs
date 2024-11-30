using doacoes.Models;
using Microsoft.EntityFrameworkCore;

namespace doacoes.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Doacao> Doacoes { get; set; }
    public DbSet<DoacaoTipo> DoacaoTipos { get; set; }
    public DbSet<Doador> Doadores { get; set; }
    public DbSet<Instituicao> Instituicoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("InMemoryDb");
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Doacao>(entity =>
        {
            // entity.Property(o => o.DoacaoTipo)
            //     .HasConversion<string>();

            entity.HasIndex(d => d.Id).IsUnique();

            entity.HasOne(x => x.Doador);
            entity.HasOne(x => x.Instituicao);
            entity.HasOne(x => x.DoacaoTipo);
        });

        modelBuilder.Entity<Doador>(entity =>
        {
            entity.HasIndex(d => d.Id)
                .IsUnique();

            entity.HasMany(x => x.Doacao);
        });

        modelBuilder.Entity<DoacaoTipo>(entity =>
        {
            entity.HasIndex(dt => dt.Id)
                .IsUnique();
        });

        modelBuilder.Entity<Instituicao>(entity => { entity.HasIndex(p => p.Id).IsUnique(); });
    }
}