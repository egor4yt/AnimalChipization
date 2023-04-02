using AnimalChipization.Core.Exceptions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Services.Interfaces;

namespace AnimalChipization.Services.Services;

public class AreaService : IAreaService
{
    private readonly IAreaRepository _areaRepository;

    public AreaService(IAreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }

    public async Task<Area?> GetByIdAsync(long areaId)
    {
        return await _areaRepository.FirstOrDefaultFullAsync(x => x.Id == areaId);
    }

    public async Task CreateAsync(Area area)
    {
        if (area == null) throw new BadRequestException("Area was null");
        await _areaRepository.InsertAsync(area);
    }
}