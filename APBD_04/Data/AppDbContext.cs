using APBD_04.Entity;
using Microsoft.EntityFrameworkCore;

namespace APBD_04.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pc> Pcs { get; set; } = null!;
    public DbSet<Component> Components { get; set; } = null!;
    public DbSet<ComponentType> ComponentTypes { get; set; } = null!;
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; } = null!;
    public DbSet<PcComponent> PcComponents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PcComponent>(entity =>
        {
            entity.HasKey(pcComponent => new
            {
                pcComponent.PcId,
                pcComponent.ComponentCode
            });

            entity.HasOne(pcComponent => pcComponent.Pc)
                .WithMany(pc => pc.PcComponents)
                .HasForeignKey(pcComponent => pcComponent.PcId);

            entity.HasOne(pcComponent => pcComponent.Component)
                .WithMany(component => component.PcComponents)
                .HasForeignKey(pcComponent => pcComponent.ComponentCode);
        });
    }
}