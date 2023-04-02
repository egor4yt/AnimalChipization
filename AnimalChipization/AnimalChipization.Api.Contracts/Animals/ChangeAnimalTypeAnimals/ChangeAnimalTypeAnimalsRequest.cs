using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.Validation;

namespace AnimalChipization.Api.Contracts.Animals.ChangeAnimalTypeAnimals;

public class ChangeAnimalTypeAnimalsRequest
{
    [Required]
    [GreaterThan(0)]
    public long OldTypeId { get; set; }

    [Required]
    [GreaterThan(0)]
    public long NewTypeId { get; set; }
}