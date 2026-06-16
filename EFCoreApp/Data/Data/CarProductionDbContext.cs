using Data.Entities;
using Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class CarProductionDbContext(DbContextOptions<CarProductionDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }

    public DbSet<Manufacturer> Manufacturers { get; set; }

    public DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=CarProduction.db;Mode=ReadWriteCreate;Cache=Shared");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const string CarIdColumnName = "car_id";
        const string ServiceIdColumnName = "service_id";

        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("cars", t =>
                t.HasCheckConstraint(
                    "valid_car_type",
                    $"car_type >= 0 AND car_type <= {Enum.GetValues(typeof(CarType)).Length - 1}"));

            entity.HasKey(c => c.VanId);

            entity.Property(c => c.VanId)
                .HasColumnName("van_id");

            entity.Property(c => c.ManufacturerId)
                .HasColumnName("manufacturer_id")
                .IsRequired();

            entity.Property(c => c.Model)
                .HasColumnName("model")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(c => c.PlateNumber)
                .HasColumnName("plate_number")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(c => c.CarType)
                .HasColumnName("car_type")
                .IsRequired();

            entity.HasOne(c => c.Manufacturer)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ManufacturerId)
                .IsRequired();
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.ToTable("manufacturers");

            entity.HasKey(m => m.Id);

            entity.Property(m => m.Id)
                .HasColumnName("id");

            entity.Property(m => m.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(m => m.Address)
                .HasColumnName("address")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(m => m.IsAChildCompany)
                .HasColumnName("is_a_child_company")
                .IsRequired();
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("services");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .HasColumnName("id");

            entity.Property(s => s.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(s => s.Address)
                .HasColumnName("address")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(s => s.IsServiceWorking)
                .HasColumnName("is_service_working")
                .IsRequired();
        });

        modelBuilder.Entity<Car>()
            .HasMany(c => c.Services)
            .WithMany(s => s.Cars)
            .UsingEntity<Dictionary<string, object>>(
                "car_services",
                service => service
                    .HasOne<Service>()
                    .WithMany()
                    .HasForeignKey(ServiceIdColumnName)
                    .HasConstraintName("fk_car_services_services_service_id")
                    .OnDelete(DeleteBehavior.Cascade),
                car => car
                    .HasOne<Car>()
                    .WithMany()
                    .HasForeignKey(CarIdColumnName)
                    .HasConstraintName("fk_car_services_cars_car_id")
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.ToTable("car_services");

                    join.HasKey(CarIdColumnName, ServiceIdColumnName);

                    join.IndexerProperty<int>(CarIdColumnName)
                        .HasColumnName(CarIdColumnName);

                    join.IndexerProperty<int>(ServiceIdColumnName)
                        .HasColumnName(ServiceIdColumnName);
                });
    }
}
