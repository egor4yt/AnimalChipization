using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.Animal;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAnimalService
{
    Task<Animal?> GetByIdAsync(long animalId);
    Task<Animal> CreateAsync(CreateAnimalModel model);
    Task<IEnumerable<Animal>> SearchAsync(SearchAnimalModel model);
    Task<Animal> UpdateAsync(UpdateAnimalModel model);
    Task<Animal> AttachAnimalTypeAsync(long animalId, long animalTypeId);
}