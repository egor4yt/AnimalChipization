using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.Location;

namespace AnimalChipization.Services.Services.Interfaces;

public interface ILocationService
{
    Task<Location?> GetByIdAsync(int locationId);
    Task CreateAsync(Location model);
    Task<Location> UpdateAsync(UpdateLocationModel model);
}