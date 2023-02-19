namespace AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Update;

public class UpdateAnimalsVisitedLocationsResponse
{
    public long Id { get; set; }
    public DateTime DateTimeOfVisitLocationPoint { get; set; }
    public long LocationPointId { get; set; }
}