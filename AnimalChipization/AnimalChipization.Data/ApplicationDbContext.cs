using AnimalChipization.Core.Helpers;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
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
    public DbSet<Area> Areas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("postgis");

        UpdateStructure(builder);
        SeedData(builder);

        base.OnModelCreating(builder);
    }

    private static void UpdateStructure(ModelBuilder builder)
    {
        builder.Entity<Account>().Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

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
    }

    private static void SeedData(ModelBuilder builder)
    {
        var accounts = new List<Account>
        {
            new()
            {
                Id = 1,
                FirstName = "adminFirstName",
                LastName = "adminLastName",
                Email = "admin@simbirsoft.com",
                Role = AccountRole.Administrator,
                PasswordHash = SecurityHelper.ComputeSha256Hash("qwerty123"),
                Animals = new List<Animal>()
            },
            new()
            {
                Id = 2,
                FirstName = "chipperFirstName",
                LastName = "chipperLastName",
                Email = "chipper@simbirsoft.com",
                Role = AccountRole.Chipper,
                PasswordHash = SecurityHelper.ComputeSha256Hash("qwerty123"),
                Animals = new List<Animal>()
            },
            new()
            {
                Id = 3,
                FirstName = "userFirstName",
                LastName = "userLastName",
                Email = "user@simbirsoft.com",
                Role = AccountRole.User,
                PasswordHash = SecurityHelper.ComputeSha256Hash("qwerty123"),
                Animals = new List<Animal>()
            }
        };

        builder.Entity<Account>().HasData(accounts);
    }
}