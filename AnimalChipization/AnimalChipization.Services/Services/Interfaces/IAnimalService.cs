using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.Animal;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAnimalService
{
    Task<Animal> CreateAsync(CreateAnimalModel model);
}