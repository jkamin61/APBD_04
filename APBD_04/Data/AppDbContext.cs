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

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.Property(m => m.Abbreviation).HasMaxLength(30).IsRequired();
            entity.Property(m => m.FullName).HasMaxLength(300).IsRequired();
            entity.Property(m => m.FoundationDate).HasColumnType("date");
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.Property(t => t.Abbreviation).HasMaxLength(30).IsRequired();
            entity.Property(t => t.Name).HasMaxLength(150).IsRequired();
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.Property(c => c.Code).HasColumnType("char(10)").HasMaxLength(10);
            entity.Property(c => c.Name).HasMaxLength(300).IsRequired();
            entity.Property(c => c.Description).HasColumnType("nvarchar(max)").IsRequired();

            entity.HasOne(c => c.ComponentManufacturer)
                .WithMany(m => m.Components)
                .HasForeignKey(c => c.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.ComponentType)
                .WithMany(t => t.Components)
                .HasForeignKey(c => c.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.Property(p => p.Name).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Weight).HasColumnType("float(5)");
            entity.Property(p => p.CreatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<PcComponent>(entity =>
        {
            entity.HasKey(pc => new { pc.PcId, pc.ComponentCode });

            entity.Property(pc => pc.ComponentCode).HasColumnType("char(10)").HasMaxLength(10);
            entity.Property(pc => pc.Amount).IsRequired();

            entity.HasOne(pc => pc.Pc)
                .WithMany(p => p.PcComponents)
                .HasForeignKey(pc => pc.PcId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pc => pc.Component)
                .WithMany(c => c.PcComponents)
                .HasForeignKey(pc => pc.ComponentCode)
                .OnDelete(DeleteBehavior.Restrict);
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices",
                FoundationDate = new DateTime(1969, 5, 1)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "NV",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateTime(1993, 4, 5)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "COR",
                FullName = "Corsair Gaming Inc.",
                FoundationDate = new DateTime(1994, 1, 1)
            });

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" });

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU0000001",
                Name = "Ryzen 7 7800X3D",
                Description = "8-core gaming processor",
                ComponentManufacturersId = 1,
                ComponentTypesId = 1
            },
            new Component
            {
                Code = "GPU0000001",
                Name = "RTX 4080 Super",
                Description = "High-end gaming graphics card",
                ComponentManufacturersId = 2,
                ComponentTypesId = 2
            },
            new Component
            {
                Code = "RAM0000001",
                Name = "Corsair Vengeance DDR5 16GB",
                Description = "DDR5 RAM module 16GB",
                ComponentManufacturersId = 3,
                ComponentTypesId = 3
            },
            new Component
            {
                Code = "CPU0000002",
                Name = "Intel Core i5-13400",
                Description = "Mid-range office processor",
                ComponentManufacturersId = 1,
                ComponentTypesId = 1
            },
            new Component
            {
                Code = "SSD0000001",
                Name = "Samsung 990 Pro 1TB",
                Description = "NVMe SSD 1TB",
                ComponentManufacturersId = 2,
                ComponentTypesId = 1
            });

        modelBuilder.Entity<Pc>().HasData(
            new Pc
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new Pc
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new Pc
            {
                Id = 3,
                Name = "Workstation Ultra",
                Weight = 15.8,
                Warranty = 48,
                CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0),
                Stock = 3
            });

        modelBuilder.Entity<PcComponent>().HasData(
            new PcComponent { PcId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new PcComponent { PcId = 1, ComponentCode = "GPU0000001", Amount = 1 },
            new PcComponent { PcId = 1, ComponentCode = "RAM0000001", Amount = 2 },
            new PcComponent { PcId = 2, ComponentCode = "CPU0000002", Amount = 1 },
            new PcComponent { PcId = 2, ComponentCode = "RAM0000001", Amount = 1 },
            new PcComponent { PcId = 3, ComponentCode = "CPU0000001", Amount = 1 },
            new PcComponent { PcId = 3, ComponentCode = "GPU0000001", Amount = 1 },
            new PcComponent { PcId = 3, ComponentCode = "SSD0000001", Amount = 2 });
    }
}
