namespace AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Get;

public class GetAnimalsVisitedLocationsResponseItem
{
    public long Id { get; set; }
    public DateTime DateTimeOfVisitLocationPoint { get; set; }
    public long LocationPointId { get; set; }
}