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
        var exists = await _areaRepository.ExistsAsync(x => x.AreaPoints.Within(area.AreaPoints) || x.AreaPoints.Contains(area.AreaPoints) || x.AreaPoints.Intersects(area.AreaPoints));
        // if (exists) throw new BadRequestException("New area should not intersect with old areas");
        
        // все кто intersect нужно получать и на стороне .NET прочекать там просто пересечение, или прям вход в зону. если просто пересечение, то можно создать
        
        await _areaRepository.InsertAsync(area);
    }

    public async Task DeleteAsync(long areaId)
    {
        var area = await _areaRepository.FirstOrDefaultFullAsync(x => x.Id == areaId);
        if (area == null) throw new NotFoundException($"Area with id {areaId} does not exists");
        await _areaRepository.DeleteAsync(area);
    }
}