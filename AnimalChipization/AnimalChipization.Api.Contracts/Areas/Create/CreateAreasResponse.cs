using AnimalChipization.Api.Contracts.Shared;

namespace AnimalChipization.Api.Contracts.Areas.Create;

public class CreateAreasResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<CoordinatesRequestItem> AreaPoints { get; set; }
}