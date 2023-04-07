using AnimalChipization.Core.Exceptions;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.Analytics;
using AnimalChipization.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        //todo add to validation attributes
        if (model.StartDate >= model.EndDate) throw new BadRequestException("Invalid date");

        var response = new AnalyzeAnimalsMovementResponse();
        response.TotalQuantityAnimals = 0;
        response.AnimalsAnalytics = new List<AnalyzeAnimalsMovementResponseItem>();

        var area = await _areaRepository.FindFirstOrDefaultAsync(x => x.Id == model.AreaId);
        if (area is null) throw new NotFoundException($"Area with id {model.AreaId} does not exists");

        //todo: PLEASE! optimize this query
        var animalsSearchRequest = _animalRepository.AsQueryable()
            .Include(x => x.ChippingLocation)
            .Include(x => x.AnimalTypes)
            .Include(x => x.AnimalVisitedLocations)
            .ThenInclude(x => x.Location)
            .Where(animal =>
                animal.AnimalVisitedLocations
                    .Where(location => location.CreatedAt > model.StartDate
                                       && location.CreatedAt < model.EndDate)
                    .Any(location => location.Location.Point.Intersects(area.AreaPoints))
                || (animal.CreatedAt > model.StartDate
                    && animal.CreatedAt < model.EndDate
                    && animal.ChippingLocation.Point.Intersects(area.AreaPoints)));

        var animalsInAreaAllTime = await animalsSearchRequest.ToListAsync();
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
                .Select(x => x.Location.Point)
                .ToList();
            trail.Insert(0, animal.ChippingLocation.Point);

            if (trail.Last().Intersects(area.AreaPoints))
            {
                response.TotalQuantityAnimals++;
                animalsAnalytics.ForEach(x => x.QuantityAnimals++);
            }

            var firstIndexOutsideArea = trail.FindIndex(x => x.Intersects(area.AreaPoints) == false);
            var firstIndexInsideArea = trail.FindIndex(x => x.Intersects(area.AreaPoints));
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