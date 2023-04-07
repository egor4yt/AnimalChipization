using AnimalChipization.Core.Exceptions;
using AnimalChipization.Core.Helpers;
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
        return await _areaRepository.FindFirstOrDefaultAsync(x => x.Id == areaId);
    }

    public async Task CreateAsync(Area area)
    {
        if (area == null) throw new BadRequestException("Area was null");

        var newAreaPolygon = GeometryHelper.PolygonFromString(area.AreaPoints);
        var allAreas = await _areaRepository.FindAllAsync(x => true);

        var exists = allAreas.Any(x =>
            GeometryHelper.PolygonFromString(x.AreaPoints).Within(newAreaPolygon)
            || GeometryHelper.PolygonFromString(x.AreaPoints).Contains(newAreaPolygon)
        );
        if (exists) throw new BadRequestException("New area should not intersect with old areas");

        var intersectedAreas = allAreas.Where(x => GeometryHelper.PolygonFromString(x.AreaPoints).Intersects(newAreaPolygon)).ToList();
        var geometries = intersectedAreas.Select(x => GeometryHelper.PolygonFromString(x.AreaPoints).Intersection(newAreaPolygon)).ToList();
        if (geometries.Any(x => x is Polygon)) throw new BadRequestException("New area should not intersect with old areas");

        await _areaRepository.InsertAsync(area);
    }

    public async Task DeleteAsync(long areaId)
    {
        var area = await _areaRepository.FindFirstOrDefaultAsync(x => x.Id == areaId);
        if (area == null) throw new NotFoundException($"Area with id {areaId} does not exists");
        await _areaRepository.DeleteAsync(area);
    }

    public async Task<Area> UpdateAsync(UpdateAreaModel model)
    {
        var area = await _areaRepository.FindFirstOrDefaultAsync(x => x.Id == model.Id);
        if (area == null) throw new BadRequestException($"Area with id {model.Id} does not exists");
        area.AreaPoints = model.AreaPoints;
        var updatingAreaPolygon = GeometryHelper.PolygonFromString(model.AreaPoints);
        var allAreas = await _areaRepository.FindAllAsync(x => true);


        var intersects = allAreas.Any(x => x.Id != model.Id
                                           && (GeometryHelper.PolygonFromString(x.AreaPoints).Within(updatingAreaPolygon)
                                               || GeometryHelper.PolygonFromString(x.AreaPoints).Contains(updatingAreaPolygon)));

        if (intersects) throw new BadRequestException("Updating area should not intersect with other areas");

        var intersectedAreas = allAreas.Where(x => x.Id != model.Id
                                                   && GeometryHelper.PolygonFromString(x.AreaPoints).Intersects(updatingAreaPolygon)).ToList();
        var geometries = intersectedAreas.Select(x => GeometryHelper.PolygonFromString(x.AreaPoints).Intersection(updatingAreaPolygon)).ToList();
        if (geometries.Any(x => x is Polygon)) throw new BadRequestException("Updating area should not intersect with other areas");

        var exists = await _areaRepository.ExistsAsync(x => x.Id != model.Id && x.Name == model.Name);
        if (exists) throw new ConflictException("Similar area already exists");

        return await _areaRepository.UpdateAsync(area);
    }
}