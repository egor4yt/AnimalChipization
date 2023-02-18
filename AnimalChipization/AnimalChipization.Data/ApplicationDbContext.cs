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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AnimalTypeAnimal>()
            .HasKey(animalTypeAnimal => new { animalTypeAnimal.AnimalId, AccountId = animalTypeAnimal.AnimalTypeId });

        builder.Entity<Animal>()
            .HasOne(animal => animal.Account)
            .WithMany(account => account.Animals)
            .HasForeignKey(animal => animal.ChipperId);

        builder.Entity<Animal>()
            .HasMany(animalType => animalType.AnimalTypes)
            .WithMany(animal => animal.Animals)
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
    }
}