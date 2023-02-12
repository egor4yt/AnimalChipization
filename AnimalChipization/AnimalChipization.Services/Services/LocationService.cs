using System.Net;
using AnimalChipization.Core.Exceptions.Account;
using AnimalChipization.Core.Exceptions.Location;
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

    public async Task<Location?> GetByIdAsync(int locationId)
    {
        if (locationId <= 0) throw new LocationGetByIdException("Invalid location id", HttpStatusCode.BadRequest);
        return await _locationRepository.FindFirstOrDefaultAsync(x => x.Id == locationId);
    }

    public async Task CreateAsync(Location location)
    {
        if (location == null) throw new LocationCreateException("Location was null", HttpStatusCode.BadRequest);
        var locationExists = await _locationRepository.ExistsAsync(x =>
            Math.Abs(x.Longitude - location.Longitude) < 0.0001
            && Math.Abs(x.Latitude - location.Latitude) < 0.0001);
        if (locationExists)
            throw new AccountRegisterException(
                $"Location with longitude: {location.Longitude} and latitude: {location.Latitude} already exists",
                HttpStatusCode.Conflict);
        
        await _locationRepository.InsertAsync(location);
    }

    public async Task<Location> UpdateAsync(UpdateLocationModel model)
    {
        if (model.Id <= 0) throw new LocationUpdateException("Invalid location id", HttpStatusCode.BadRequest);

        var location = await _locationRepository.FindFirstOrDefaultAsync(x => x.Id == model.Id);
        if (location == null)
            throw new LocationUpdateException($"Location with id {model.Id} does not exists", HttpStatusCode.NotFound);

        var locationExists = await _locationRepository.ExistsAsync(x =>
            Math.Abs(x.Longitude - model.Longitude) < 0.01
            && Math.Abs(x.Latitude - model.Latitude) < 0.01
            && x.Id != model.Id);
        if (locationExists)
            throw new AccountRegisterException(
                $"Location with longitude: {model.Longitude} and latitude: {model.Latitude} already exists",
                HttpStatusCode.Conflict);

        location.Longitude = model.Longitude;
        location.Latitude = model.Latitude;

        return await _locationRepository.UpdateAsync(location);
    }
}