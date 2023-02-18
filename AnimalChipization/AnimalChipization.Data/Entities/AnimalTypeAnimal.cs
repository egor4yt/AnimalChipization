using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Data.Entities;

public class AnimalTypeAnimal : EntityBase
{
    [Required]
    public long AnimalId { get; set; }

    public virtual Animal Animal { get; set; }

    [Required]
    public long AnimalTypeId { get; set; }

    public virtual AnimalType AnimalType { get; set; }
}