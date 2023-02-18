using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.AnimalType;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAnimalTypeService
{
    Task<AnimalType?> GetByIdAsync(long animalTypeId);

    Task CreateAsync(AnimalType animalType);
    Task<AnimalType?> UpdateAsync(UpdateAnimalTypeModel model);
    Task DeleteAsync(long animalTypeId);
}