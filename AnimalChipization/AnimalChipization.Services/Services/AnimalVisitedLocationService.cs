using System.Net;
using AnimalChipization.Core.Exceptions.AnimalVisitedLocation;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.AnimalVisitedLocation;
using AnimalChipization.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<AnimalVisitedLocation>> GetAsync(GetAnimalVisitedLocationModel model)
    {
        var animalExists = await _animalRepository.ExistsAsync(x => x.Id == model.AnimalId);
        if (animalExists == false) throw new AnimalVisitedLocationGetException($"Animal with id {model.AnimalId} does not exists", HttpStatusCode.NotFound);
        
        var animalVisitedLocations = _animalVisitedLocationRepository.AsQueryable();

        animalVisitedLocations = animalVisitedLocations.Where(x => x.AnimalId == model.AnimalId);
        if (model.StartDateTime is not null) animalVisitedLocations = animalVisitedLocations.Where(x => x.CreatedAt >= model.StartDateTime);
        if (model.EndDateTime is not null) animalVisitedLocations = animalVisitedLocations.Where(x => x.CreatedAt <= model.EndDateTime);

        return await animalVisitedLocations
            .OrderBy(x => x.CreatedAt)
            .Skip(model.From)
            .Take(model.Size)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<AnimalVisitedLocation> AddAsync(long animalId, long pointId)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == animalId);
        if (animal == null) throw new AnimalVisitedLocationAddException($"Animal with id {animalId} does not exists", HttpStatusCode.NotFound);

        if (animal.LifeStatus == LifeStatus.Dead) throw new AnimalVisitedLocationAddException($"Animal with id {animalId} is dead", HttpStatusCode.BadRequest);
        if (animal.AnimalVisitedLocations.Count == 0 && animal.ChippingLocationId == pointId) throw new AnimalVisitedLocationAddException("First visited location cant be chipping location", HttpStatusCode.BadRequest);

        var locationExists = await _locationRepository.ExistsAsync(x => x.Id == pointId);
        if (locationExists == false) throw new AnimalVisitedLocationAddException($"Location with id {pointId} does not exists", HttpStatusCode.NotFound);
        if (animal.AnimalVisitedLocations.Count > 0 && animal.AnimalVisitedLocations.Last().LocationId == pointId) throw new AnimalVisitedLocationAddException($"Animal already in location with id {pointId}", HttpStatusCode.BadRequest);

        var newAnimalVisitedLocation = new AnimalVisitedLocation
        {
            AnimalId = animal.Id,
            LocationId = pointId
        };
        await _animalVisitedLocationRepository.InsertAsync(newAnimalVisitedLocation);
        return newAnimalVisitedLocation;
    }

    public async Task DeleteAsync(long animalId, long visitedPointId)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == animalId);
        if (animal == null) throw new AnimalVisitedLocationDeleteException($"Animal with id {animalId} does not exists", HttpStatusCode.NotFound);

        var visitedLocationExists = animal.AnimalVisitedLocations.Any(x => x.Id == visitedPointId);
        if (visitedLocationExists == false) throw new AnimalVisitedLocationDeleteException($"Animal with id {animalId} does not have visited location with id {visitedPointId}", HttpStatusCode.NotFound);

        var visitedLocationsToDelete = new List<AnimalVisitedLocation>();

        if (animal.AnimalVisitedLocations.Count >= 2
            && animal.AnimalVisitedLocations[0].Id == visitedPointId
            && animal.AnimalVisitedLocations[1].LocationId == animal.ChippingLocationId) visitedLocationsToDelete.Add(animal.AnimalVisitedLocations[1]);

        visitedLocationsToDelete.Add(animal.AnimalVisitedLocations.First(x => x.Id == visitedPointId));

        await _animalVisitedLocationRepository.DeleteRangeAsync(visitedLocationsToDelete);
    }

    public async Task<AnimalVisitedLocation> UpdateAsync(UpdateAnimalVisitedLocationModel model)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == model.AnimalId);
        if (animal == null) throw new AnimalVisitedLocationUpdateException($"Animal with id {model.AnimalId} does not exists", HttpStatusCode.NotFound);

        var locationExists = await _locationRepository.ExistsAsync(x => x.Id == model.LocationPointId);
        if (locationExists == false) throw new AnimalVisitedLocationUpdateException($"Location with id {model.LocationPointId} does not exists", HttpStatusCode.NotFound);
        
        var visitedLocation = animal.AnimalVisitedLocations.FirstOrDefault(x => x.Id == model.VisitedLocationPointId);
        if (animal.AnimalVisitedLocations.Count == 0 || visitedLocation == null) throw new AnimalVisitedLocationUpdateException($"Visited location with id {model.VisitedLocationPointId} does not exists", HttpStatusCode.NotFound);

        if (animal.ChippingLocationId == model.LocationPointId) throw new AnimalVisitedLocationUpdateException("First visited location can't be chipping location", HttpStatusCode.BadRequest);
        if (visitedLocation.LocationId == model.LocationPointId) throw new AnimalVisitedLocationUpdateException($"Visited location already is {model.LocationPointId}", HttpStatusCode.BadRequest);

        var visitedLocationIndex = animal.AnimalVisitedLocations.IndexOf(visitedLocation);
        if (visitedLocationIndex > 0 && animal.AnimalVisitedLocations[visitedLocationIndex - 1].LocationId == model.LocationPointId) throw new AnimalVisitedLocationUpdateException("Previous location has the same id", HttpStatusCode.BadRequest);
        if (visitedLocationIndex < animal.AnimalVisitedLocations.Count - 1 && animal.AnimalVisitedLocations[visitedLocationIndex + 1].LocationId == model.LocationPointId) throw new AnimalVisitedLocationUpdateException("Following location has the same id", HttpStatusCode.BadRequest);
        if (visitedLocation.LocationId == model.LocationPointId) throw new AnimalVisitedLocationUpdateException($"Visited location already is {model.LocationPointId}", HttpStatusCode.BadRequest);

        visitedLocation.LocationId = model.LocationPointId;

        return await _animalVisitedLocationRepository.UpdateAsync(visitedLocation);
    }
}