using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.AnimalVisitedLocation;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAnimalVisitedLocationService
{
    Task<AnimalVisitedLocation> AddAsync(long animalId, long pointId);
    Task DeleteAsync(long animalId, long visitedPointId);
    Task<AnimalVisitedLocation> UpdateAsync(UpdateAnimalVisitedLocationModel model);
}