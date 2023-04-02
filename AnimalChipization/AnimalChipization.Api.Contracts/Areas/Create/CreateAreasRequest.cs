using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.Shared;
using AnimalChipization.Api.Contracts.Validation;

namespace AnimalChipization.Api.Contracts.Areas.Create;

public class CreateAreasRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    [CoordinatesCollection]
    public List<CoordinatesRequestItem> AreaPoints { get; set; }
}