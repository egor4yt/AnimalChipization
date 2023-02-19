using System.Net;
using AnimalChipization.Core.Exceptions.AnimalVisitedLocation;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Services.Interfaces;

namespace AnimalChipization.Services.Services;

public class AnimalVisitedLocationService : IAnimalVisitedLocationService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalVisitedLocationRepository _animalVisitedLocationRepository;
    private readonly ILocationRepository _locationRepository;

    public AnimalVisitedLocationService(IAnimalVisitedLocationRepository animalVisitedLocationRepository, IAnimalRepository animalRepository, ILocationRepository locationRepository)
    {
        _animalVisitedLocationRepository = animalVisitedLocationRepository;
        _animalRepository = animalRepository;
        _locationRepository = locationRepository;
    }

    public async Task<AnimalVisitedLocation> AddAsync(long animalId, long pointId)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == animalId);
        if (animal == null) throw new AnimalVisitedLocationAddException($"Animal with id {animalId} does not exists", HttpStatusCode.NotFound);

        if (animal.LifeStatus == LifeStatus.Dead) throw new AnimalVisitedLocationAddException($"Animal with id {animalId} is dead", HttpStatusCode.BadRequest);
        if (animal.AnimalVisitedLocations.Count == 0 && animal.ChippingLocationId == pointId) throw new AnimalVisitedLocationAddException("First visited location cant be chipping location", HttpStatusCode.BadRequest);

        var location = await _locationRepository.FindFirstOrDefaultAsync(x => x.Id == pointId);
        if (location == null) throw new AnimalVisitedLocationAddException($"Location with id {pointId} does not exists", HttpStatusCode.NotFound);
        if (animal.AnimalVisitedLocations.Count > 0 && animal.AnimalVisitedLocations.Last().LocationId == location.Id) throw new AnimalVisitedLocationAddException($"Animal already in location with id {location.Id}", HttpStatusCode.BadRequest);

        var newAnimalVisitedLocation = new AnimalVisitedLocation
        {
            AnimalId = animal.Id,
            LocationId = location.Id
        };
        await _animalVisitedLocationRepository.InsertAsync(newAnimalVisitedLocation);
        return newAnimalVisitedLocation;
    }

    public async Task DeleteAsync(long animalId, long visitedPointId)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == animalId);
        if (animal == null) throw new AnimalVisitedLocationDeleteAsyncException($"Animal with id {animalId} does not exists", HttpStatusCode.NotFound);

        var visitedLocationExists = animal.AnimalVisitedLocations.Any(x => x.Id == visitedPointId);
        if (visitedLocationExists == false) throw new AnimalVisitedLocationDeleteAsyncException($"Animal with id {animalId} does not have visited location with id {visitedPointId}", HttpStatusCode.NotFound);

        var visitedLocationsToDelete = new List<AnimalVisitedLocation>();

        if (animal.AnimalVisitedLocations.Count >= 2
            && animal.AnimalVisitedLocations[0].Id == visitedPointId
            && animal.AnimalVisitedLocations[1].LocationId == animal.ChippingLocationId) visitedLocationsToDelete.Add(animal.AnimalVisitedLocations[1]);

        visitedLocationsToDelete.Add(animal.AnimalVisitedLocations.First(x => x.Id == visitedPointId));

        await _animalVisitedLocationRepository.DeleteRangeAsync(visitedLocationsToDelete);
    }
}