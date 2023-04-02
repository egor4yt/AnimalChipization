using AnimalChipization.Api.Contracts.Validation;

namespace AnimalChipization.Api.Contracts.Paging;

public class PagingSettings
{
    [GreaterThan(-1)]
    public int From { get; set; } = 0;

    [GreaterThan(0)]
    public int Size { get; set; } = 10;
}