namespace AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Add;

public class AddAnimalsVisitedLocationsResponse
{
    public long Id { get; set; }
    public string DateTimeOfVisitLocationPoint { get; set; }
    public long LocationPointId { get; set; }
}