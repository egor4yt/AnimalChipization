using NetTopologySuite.Geometries;

namespace AnimalChipization.Services.Models.Area;

public class UpdateAreaModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Polygon AreaPoints { get; set; }
}