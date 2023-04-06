using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.Shared;
using AnimalChipization.Api.Contracts.Validation;

namespace AnimalChipization.Api.Contracts.Areas.Update;

public class UpdateAreasRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    [PolygonCoordinatesCollection]
    public List<CoordinatesRequestItem> AreaPoints { get; set; }
}