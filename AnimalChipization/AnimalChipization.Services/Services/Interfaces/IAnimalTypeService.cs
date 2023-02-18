using AnimalChipization.Data.Entities;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAnimalTypeService
{
    Task CreateAsync(AnimalType animalType);
}