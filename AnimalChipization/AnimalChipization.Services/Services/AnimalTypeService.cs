using System.Net;
using AnimalChipization.Core.Exceptions.AnimalType;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.AnimalType;
using AnimalChipization.Services.Services.Interfaces;

namespace AnimalChipization.Services.Services;

public class AnimalTypeService : IAnimalTypeService
{
    private readonly IAnimalTypeRepository _animalTypeRepository;

    public AnimalTypeService(IAnimalTypeRepository animalTypeRepository)
    {
        _animalTypeRepository = animalTypeRepository;
    }

    public async Task<AnimalType?> GetByIdAsync(long animalTypeId)
    {
        return await _animalTypeRepository.FindFirstOrDefaultAsync(x => x.Id == animalTypeId);
    }

    public async Task CreateAsync(AnimalType animalType)
    {
        if (animalType == null) throw new AnimalTypeCreateException("Animal type was null", HttpStatusCode.BadRequest);

        animalType.Type = animalType.Type.ToLower().Trim();
        var animalTypeExists = await _animalTypeRepository.ExistsAsync(x => x.Type.ToLower() == animalType.Type);
        if (animalTypeExists) throw new AnimalTypeCreateException($"Animal type with type: {animalType.Type} already exists", HttpStatusCode.Conflict);

        await _animalTypeRepository.InsertAsync(animalType);
    }

    public async Task<AnimalType?> UpdateAsync(UpdateAnimalTypeModel model)
    {
        model.Type = model.Type.ToLower().Trim();

        var animalType = await _animalTypeRepository.FindFirstOrDefaultAsync(x => x.Id == model.Id);
        if (animalType == null) throw new AnimalTypeUpdateException($"Animal type with id {model.Id} does not exists", HttpStatusCode.NotFound);

        var locationExists = await _animalTypeRepository.ExistsAsync(x =>
            x.Type.ToLower() == model.Type
            && x.Id != model.Id
        );
        if (locationExists) throw new AnimalTypeUpdateException($"Animal type with type: {model.Type} already exists", HttpStatusCode.Conflict);

        animalType.Type = model.Type;

        return await _animalTypeRepository.UpdateAsync(animalType);
    }
}