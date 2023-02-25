using AnimalChipization.Api.Contracts.Paging;

namespace AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Get;

public class GetAnimalsVisitedLocationsRequest : PagingSettings
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}