namespace AnimalChipization.Services.Models.AnimalVisitedLocation;

public class UpdateAnimalVisitedLocationModel
{
    public long AnimalId { get; set; }
    public long VisitedLocationPointId { get; set; }
    public long LocationPointId { get; set; }
}