using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.Shared;

public class CoordinatesRequestItem
{
    [Range(-180f, 180f)]
    public double Longitude { get; set; }

    [Range(-90f, 90f)]
    public double Latitude { get; set; }
}