using NetTopologySuite.Geometries;

namespace AnimalChipization.Services.Models.Location;

public class UpdateLocationModel
{
    public long Id { get; set; }
    public Point Point { get; set; }
}