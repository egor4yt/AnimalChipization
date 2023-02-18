using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.Locations.Update;

public class UpdateLocationsRequest
{
    [Range(-180f, 180f)]
    public double Longitude { get; set; }

    [Range(-90f, 90f)]
    public double Latitude { get; set; }
}