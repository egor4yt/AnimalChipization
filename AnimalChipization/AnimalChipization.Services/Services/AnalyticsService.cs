using AnimalChipization.Core.Exceptions;
using AnimalChipization.Core.Helpers;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.Analytics;
using AnimalChipization.Services.Services.Interfaces;
using NetTopologySuite.Geometries;

namespace AnimalChipization.Services.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalVisitedLocationRepository _animalVisitedLocationRepository;
    private readonly IAreaRepository _areaRepository;

    public AnalyticsService(IAnimalRepository animalRepository, IAreaRepository areaRepository, IAnimalVisitedLocationRepository animalVisitedLocationRepository)
    {
        _animalRepository = animalRepository;
        _areaRepository = areaRepository;
        _animalVisitedLocationRepository = animalVisitedLocationRepository;
    }

    public async Task<AnalyzeAnimalsMovementResponse> AnalyzeAnimalsMovement(AnalyzeAnimalsMovementModel model)
    {
        if (model.StartDate >= model.EndDate) throw new BadRequestException("Invalid date");

        var response = new AnalyzeAnimalsMovementResponse();
        response.AnimalsAnalytics = new List<AnalyzeAnimalsMovementResponseItem>();

        var area = await _areaRepository.FindFirstOrDefaultAsync(x => x.Id == model.AreaId);
        if (area is null) throw new NotFoundException($"Area with id {model.AreaId} does not exists");
        var currentAreaPolygon = GeometryHelper.PolygonFromString(area.AreaPoints);

        var allAreas = await _animalRepository.FindAllFullAsync(x => true);
        // //todo: PLEASE! optimize this query
        var animalsInAreaAllTime = allAreas
            .Where(animal =>
                animal.AnimalVisitedLocations
                    .Where(location => location.CreatedAt > model.StartDate
                                       && location.CreatedAt < model.EndDate)
                    .Any(location => new Point(new Coordinate(location.Location.Latitude, location.Location.Longitude)).Intersects(currentAreaPolygon))
                || (animal.CreatedAt > model.StartDate
                    && animal.CreatedAt < model.EndDate
                    && new Point(new Coordinate(animal.ChippingLocation.Latitude, animal.ChippingLocation.Longitude)).Intersects(currentAreaPolygon)));

        foreach (var animal in animalsInAreaAllTime)
        {
            var animalsAnalytics = animal.AnimalTypes
                .Select(animalType =>
                    response.AnimalsAnalytics.FirstOrDefault(analytics => analytics.AnimalTypeId == animalType.Id)
                    ?? new AnalyzeAnimalsMovementResponseItem
                    {
                        AnimalType = animalType.Type,
                        AnimalTypeId = animalType.Id,
                        QuantityAnimals = 0,
                        AnimalsArrived = 0,
                        AnimalsGone = 0
                    }).ToList();

            var trail = animal.AnimalVisitedLocations
                .Select(x => new Point(new Coordinate(x.Location.Latitude, x.Location.Longitude)))
                .ToList();
            trail.Insert(0, new Point(new Coordinate(animal.ChippingLocation.Latitude, animal.ChippingLocation.Longitude)));

            if (trail.Last().Intersects(currentAreaPolygon))
            {
                response.TotalQuantityAnimals++;
                animalsAnalytics.ForEach(x => x.QuantityAnimals++);
            }

            var firstIndexOutsideArea = trail.FindIndex(x => x.Intersects(currentAreaPolygon) == false);
            var firstIndexInsideArea = trail.FindIndex(x => x.Intersects(currentAreaPolygon));
            if (firstIndexOutsideArea != -1
                && firstIndexInsideArea != -1
                && firstIndexOutsideArea < firstIndexInsideArea)
            {
                response.TotalAnimalsArrived++;
                animalsAnalytics.ForEach(x => x.AnimalsArrived++);
            }

            if (firstIndexOutsideArea != -1
                && firstIndexInsideArea != -1
                && firstIndexOutsideArea > firstIndexInsideArea)
            {
                response.TotalAnimalsGone++;
                animalsAnalytics.ForEach(x => x.AnimalsGone++);
            }

            response.AnimalsAnalytics.RemoveAll(x => animalsAnalytics.Any(y => y.AnimalTypeId == x.AnimalTypeId));
            response.AnimalsAnalytics.AddRange(animalsAnalytics);
        }

        return response;
    }
}