using System.ComponentModel.DataAnnotations;
using AnimalChipization.Core.Validation;

namespace AnimalChipization.Api.Contracts.Animals.Update;

public class UpdateAnimalsRequest
{
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
    [LifeStatus(false)]
    public string LifeStatus { get; set; }
}