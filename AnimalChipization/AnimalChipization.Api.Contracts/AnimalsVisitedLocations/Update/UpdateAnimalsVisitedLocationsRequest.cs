using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.Validation;

namespace AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Update;

public class UpdateAnimalsVisitedLocationsRequest
{
    [Required]
    [GreaterThan(0L)]
    public long VisitedLocationPointId { get; set; }

    [Required]
    [GreaterThan(0L)]
    public long LocationPointId { get; set; }
}