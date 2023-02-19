namespace AnimalChipization.Api.Contracts.Animals.AddVisitedLocationAnimals;

public class AddVisitedLocationAnimalsResponse
{
    public long Id { get; set; }
    public DateTime DateTimeOfVisitLocationPoint { get; set; }
    public long LocationPointId { get; set; }
}