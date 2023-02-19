using AnimalChipization.Data.Entities;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAnimalVisitedLocationService
{
    Task<AnimalVisitedLocation> AddAsync(long animalId, long pointId);
    Task DeleteAsync(long animalId, long visitedPointId);
}