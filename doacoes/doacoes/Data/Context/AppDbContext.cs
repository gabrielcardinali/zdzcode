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
            
            // entity.ToTable(t => t.HasCheckConstraint("CHK_Orders_FkCustomerID",
            //     @"(OrderType = 'Sale' AND FkCustomerID IS NOT NULL AND FkSupplierID IS NULL) OR
            //   (OrderType = 'Purchase' AND FkSupplierID IS NOT NULL AND FkCustomerID IS NULL)"));
        });

        modelBuilder.Entity<Doador>(entity =>
        {
            entity.HasIndex(d => d.Id)
                .IsUnique();
        });

        modelBuilder.Entity<DoacaoTipo>(entity =>
        {
            entity.HasIndex(dt => dt.Id)
                .IsUnique();
        });

        modelBuilder.Entity<Instituicao>(entity =>
        {
            entity.HasIndex(p => p.Id).IsUnique();
        });
    }
}