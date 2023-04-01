using AnimalChipization.Core.Exceptions;
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
        if (animalType == null) throw new BadRequestException("Animal type was null");

        animalType.Type = animalType.Type.ToLower().Trim();
        var animalTypeExists = await _animalTypeRepository.ExistsAsync(x => x.Type.ToLower() == animalType.Type);
        if (animalTypeExists) throw new ConflictException($"Animal type with type: {animalType.Type} already exists");

        await _animalTypeRepository.InsertAsync(animalType);
    }

    public async Task<AnimalType?> UpdateAsync(UpdateAnimalTypeModel model)
    {
        model.Type = model.Type.ToLower().Trim();

        var animalType = await _animalTypeRepository.FindFirstOrDefaultAsync(x => x.Id == model.Id);
        if (animalType == null) throw new NotFoundException($"Animal type with id {model.Id} does not exists");

        var locationExists = await _animalTypeRepository.ExistsAsync(x =>
            x.Type.ToLower() == model.Type
            && x.Id != model.Id
        );
        if (locationExists) throw new ConflictException($"Animal type with type: {model.Type} already exists");

        animalType.Type = model.Type;

        return await _animalTypeRepository.UpdateAsync(animalType);
    }

    public async Task DeleteAsync(long animalTypeId)
    {
        var animalType = await _animalTypeRepository.FirstOrDefaultWithAnimalsAsync(x => x.Id == animalTypeId);
        if (animalType is null) throw new NotFoundException($"Animal type with id {animalTypeId} does not exists");
        if (animalType.Animals.Any()) throw new BadRequestException($"Animal type with id {animalTypeId} has relations with animals");

        await _animalTypeRepository.DeleteAsync(animalType);
    }
}