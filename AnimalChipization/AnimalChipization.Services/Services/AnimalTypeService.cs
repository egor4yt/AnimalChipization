using System.Net;
using AnimalChipization.Core.Exceptions.AnimalType;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Services.Interfaces;

namespace AnimalChipization.Services.Services;

public class AnimalTypeService : IAnimalTypeService
{
    private readonly IAnimalTypeRepository _animalTypeRepository;

    public AnimalTypeService(IAnimalTypeRepository animalTypeRepository)
    {
        _animalTypeRepository = animalTypeRepository;
    }

    public async Task CreateAsync(AnimalType animalType)
    {
        if (animalType == null) throw new AnimalTypeCreateException("Animal type was null", HttpStatusCode.BadRequest);

        animalType.Type = animalType.Type.ToLower();
        var animalTypeExists = await _animalTypeRepository.ExistsAsync(x => x.Type.ToLower() == animalType.Type);
        if (animalTypeExists) throw new AnimalTypeCreateException($"Animal type with type: {animalType.Type} already exists", HttpStatusCode.Conflict);

        await _animalTypeRepository.InsertAsync(animalType);
    }
}