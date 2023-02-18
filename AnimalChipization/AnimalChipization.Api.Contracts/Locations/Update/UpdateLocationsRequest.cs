using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.Locations.Update;

public class UpdateLocationsRequest
{
    [Range(-180, 180)]
    public double Longitude { get; set; }

    [Range(-90, 90)]
    public double Latitude { get; set; }
}