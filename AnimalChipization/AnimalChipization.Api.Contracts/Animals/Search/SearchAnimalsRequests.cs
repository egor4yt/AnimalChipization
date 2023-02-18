using AnimalChipization.Api.Contracts.Paging;
using AnimalChipization.Core.Validation;

namespace AnimalChipization.Api.Contracts.Animals.Search;

public class SearchAnimalsRequests : PagingSettings
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public int? ChipperId { get; set; }
    public long? ChippingLocationId { get; set; }

    [LifeStatus(true)]
    public string? LifeStatus { get; set; }

    [Gender(true)]
    public string? Gender { get; set; }
}