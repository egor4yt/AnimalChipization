using AnimalChipization.Core.Exceptions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.Area;
using AnimalChipization.Services.Services.Interfaces;
using NetTopologySuite.Geometries;

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
        var exists = await _areaRepository.ExistsAsync(x => x.AreaPoints.Within(area.AreaPoints) || x.AreaPoints.Contains(area.AreaPoints));
        if (exists) throw new BadRequestException("New area should not intersect with old areas");

        var intersectedAreas = await _areaRepository.FindAllAsync(x => x.AreaPoints.Intersects(area.AreaPoints));
        var geometries = intersectedAreas.Select(x => x.AreaPoints.Intersection(area.AreaPoints)).ToList();
        if (geometries.Any(x => x is Polygon)) throw new BadRequestException("New area should not intersect with old areas");

        await _areaRepository.InsertAsync(area);
    }

    public async Task DeleteAsync(long areaId)
    {
        var area = await _areaRepository.FirstOrDefaultFullAsync(x => x.Id == areaId);
        if (area == null) throw new NotFoundException($"Area with id {areaId} does not exists");
        await _areaRepository.DeleteAsync(area);
    }

    public async Task<Area> UpdateAsync(UpdateAreaModel model)
    {
        var area = await _areaRepository.FirstOrDefaultFullAsync(x => x.Id == model.Id);
        if (area == null) throw new BadRequestException($"Area with id {model.Id} does not exists");
        area.AreaPoints = model.AreaPoints;
        
        var exists = await _areaRepository.ExistsAsync(x => x.Id != model.Id
            && (x.AreaPoints.Within(model.AreaPoints)
                || x.AreaPoints.Contains(model.AreaPoints)));
        
        if (exists) throw new BadRequestException("Updating area should not intersect with other areas");
        
        var intersectedAreas = await _areaRepository.FindAllAsync(x => x.Id != model.Id && x.AreaPoints.Intersects(area.AreaPoints));
        var geometries = intersectedAreas.Select(x => x.AreaPoints.Intersection(area.AreaPoints)).ToList();
        if (geometries.Any(x => x is Polygon)) throw new BadRequestException("Updating area should not intersect with other areas");
        
        return await _areaRepository.UpdateAsync(area);
    }
}