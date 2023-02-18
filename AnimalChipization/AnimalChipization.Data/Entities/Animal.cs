using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Data.Entities;

public class Animal : EntityBase
{
    public long Id { get; set; }

    [Required]
    public float WeightKilograms { get; set; }

    [Required]
    public float HeightMeters { get; set; }

    [Required]
    public float LengthMeters { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    public string LifeStatus { get; set; }

    [Required]
    public DateTime ChippingDateTime { get; set; }

    public DateTime? DeathDateTime { get; set; }

    [Required]
    public long ChippingLocationId { get; set; }

    public virtual Location ChippingLocation { get; set; }

    [Required]
    public int ChipperId { get; set; }

    public virtual Account Account { get; set; }

    public virtual List<AnimalType> AnimalTypes { get; set; }
    public virtual List<AnimalTypeAnimal> AnimalTypesAnimals { get; set; }

    // todo: add chipping location
    // [Required]
    // public long ChippingLocationId { get; set; } 
    // public virtual ChippingLocation ChippingLocation { get; set; }   
}