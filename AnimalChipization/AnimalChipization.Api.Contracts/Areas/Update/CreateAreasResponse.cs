using AnimalChipization.Api.Contracts.Shared;

namespace AnimalChipization.Api.Contracts.Areas.Update;

public class UpdateAreasResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<CoordinatesRequestItem> AreaPoints { get; set; }
}