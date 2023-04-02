using AnimalChipization.Api.Contracts.Shared;

namespace AnimalChipization.Api.Contracts.Areas.GetById;

public class GetByIdAreasResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<CoordinatesRequestItem> AreaPoints { get; set; }
}