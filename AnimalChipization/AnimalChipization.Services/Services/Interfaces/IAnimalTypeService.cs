using AnimalChipization.Data.Entities;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAnimalTypeService
{
    Task<AnimalType?> GetByIdAsync(long animalTypeId);

    Task CreateAsync(AnimalType animalType);
}