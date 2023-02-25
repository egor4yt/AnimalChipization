namespace AnimalChipization.Services.Models.AnimalVisitedLocation;

public class GetAnimalVisitedLocationModel
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public long AnimalId { get; set; }
    public int From { get; set; }
    public int Size { get; set; }
}