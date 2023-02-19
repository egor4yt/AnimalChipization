using AnimalChipization.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<AnimalType> AnimalsTypes { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalTypeAnimal> AnimalTypesAnimals { get; set; }
    public DbSet<AnimalVisitedLocation> AnimalsVisitedLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AnimalTypeAnimal>()
            .HasKey(animalTypeAnimal => new { animalTypeAnimal.AnimalId, AccountId = animalTypeAnimal.AnimalTypeId });

        builder.Entity<Animal>()
            .HasOne(animal => animal.Account)
            .WithMany(account => account.Animals)
            .HasForeignKey(animal => animal.ChipperId);

        builder.Entity<Animal>()
            .HasOne(animal => animal.ChippingLocation)
            .WithMany(location => location.Animals)
            .HasForeignKey(animal => animal.ChippingLocationId);

        builder.Entity<Animal>()
            .HasMany(animal => animal.AnimalTypes)
            .WithMany(animalType => animalType.Animals)
            .UsingEntity<AnimalTypeAnimal>(
                entity => entity
                    .HasOne(p => p.AnimalType)
                    .WithMany(p => p.AnimalTypesAnimals)
                    .HasForeignKey(p => p.AnimalTypeId),
                entity => entity
                    .HasOne(p => p.Animal)
                    .WithMany(p => p.AnimalTypesAnimals)
                    .HasForeignKey(p => p.AnimalId),
                entity =>
                {
                    entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    entity.HasKey(p => new { p.AnimalId, p.AnimalTypeId });
                }
            );

        builder.Entity<AnimalVisitedLocation>()
            .HasOne(pt => pt.Location)
            .WithMany(p => p.AnimalsVisitedLocations)
            .HasForeignKey(pt => pt.LocationId);

        builder.Entity<AnimalVisitedLocation>()
            .HasOne(pt => pt.Animal)
            .WithMany(t => t.AnimalVisitedLocations)
            .HasForeignKey(pt => pt.AnimalId);


        //
        // builder.Entity<Animal>()
        //     .HasMany(animal => animal.Locations)
        //     .WithMany(locations => locations.Animals)
        //     .UsingEntity<AnimalVisitedLocation>(
        //         entity => entity
        //             .HasOne(p => p.Location)
        //             .WithMany(p => p.AnimalsVisitedLocations)
        //             .HasForeignKey(p => p.LocationId),
        //         entity => entity
        //             .HasOne(p => p.Animal)
        //             .WithMany(p => p.AnimalsVisitedLocations)
        //             .HasForeignKey(p => p.AnimalId),
        //         entity =>
        //         {
        //             entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //             entity.HasKey(p => new { p.AnimalId, p.LocationId });
        //         }
        //     );
    }
}