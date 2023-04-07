using NetTopologySuite.Geometries;

namespace AnimalChipization.Data.Entities;

public class Location : EntityBase
{
    public long Id { get; set; }
    public Point Point { get; set; }

    public List<Animal> Animals { get; set; }
    public List<AnimalVisitedLocation> AnimalsVisitedLocations { get; set; }
}