using System.ComponentModel.DataAnnotations;
using AnimalChipization.Core.Validation;

namespace AnimalChipization.Api.Contracts.Animals.Create;

public class CreateAnimalsRequest
{
    [Required]
    [MinLength(1)]
    [ListPositiveLongNumbers]
    [AllElementsUnique]
    public List<long> AnimalTypes { get; set; }

    [Required]
    [GreaterThan(0f)]
    public float Weight { get; set; }

    [Required]
    [GreaterThan(0f)]
    public float Height { get; set; }

    [Required]
    [GreaterThan(0f)]
    public float Length { get; set; }

    [Required]
    [GreaterThan(0)]
    public int ChipperId { get; set; }

    [Required]
    [GreaterThan(0L)]
    public long ChippingLocationId { get; set; }

    [Gender(false)]
    public string Gender { get; set; }
}