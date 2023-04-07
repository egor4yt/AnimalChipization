using AnimalChipization.Core.Exceptions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.Location;
using AnimalChipization.Services.Services.Interfaces;

namespace AnimalChipization.Services.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Location?> GetByIdAsync(long locationId)
    {
        return await _locationRepository.FindFirstOrDefaultAsync(x => x.Id == locationId);
    }

    public async Task CreateAsync(Location location)
    {
        if (location == null) throw new BadRequestException("Location was null");

        var locationExists = await _locationRepository.ExistsAsync(x =>
            Math.Abs(x.Latitude - location.Latitude) < 0.01
            && Math.Abs(x.Longitude - location.Longitude) < 0.01);
        if (locationExists) throw new ConflictException($"Location with longitude: {location.Latitude} and latitude: {location.Longitude} already exists");

        await _locationRepository.InsertAsync(location);
    }

    public async Task<Location> UpdateAsync(UpdateLocationModel model)
    {
        var location = await _locationRepository.FindFirstOrDefaultAsync(x => x.Id == model.Id);
        if (location == null) throw new NotFoundException($"Location with id {model.Id} does not exists");

        var locationExists = await _locationRepository.ExistsAsync(x =>
            Math.Abs(x.Latitude - model.Latitude) < 0.01
            && Math.Abs(x.Longitude - model.Longitude) < 0.01
            && x.Id != model.Id
        );
        if (locationExists) throw new ConflictException($"Location with longitude: {model.Latitude} and latitude: {model.Longitude} already exists");

        location.Latitude = model.Latitude;
        location.Longitude = model.Longitude;

        return await _locationRepository.UpdateAsync(location);
    }

    public async Task DeleteAsync(long pointId)
    {
        var location = await _locationRepository.FirstOrDefaultFullAsync(x => x.Id == pointId);
        if (location is null) throw new NotFoundException($"Location with id {pointId} does not exists");
        if (location.AnimalsVisitedLocations.Any()) throw new BadRequestException($"Location with id {pointId} visited by an animals");
        if (location.Animals.Any()) throw new BadRequestException($"Location with id {pointId} has relations with animals");

        await _locationRepository.DeleteAsync(location);
    }
}