namespace AnimalChipization.Data.Entities;

public class Location : EntityBase
{
    public long Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<Animal> Animals { get; set; }
    public List<AnimalVisitedLocation> AnimalsVisitedLocations { get; set; }
}