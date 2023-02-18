using System.Net;
using AnimalChipization.Core.Exceptions.Animal;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.Animal;
using AnimalChipization.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Services.Services;

public class AnimalService : IAnimalService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalTypeRepository _animalTypeRepository;
    private readonly ILocationRepository _locationRepository;

    public AnimalService(IAnimalTypeRepository animalTypeRepository, IAnimalRepository animalRepository, IAccountRepository accountRepository, ILocationRepository locationRepository)
    {
        _animalTypeRepository = animalTypeRepository;
        _animalRepository = animalRepository;
        _accountRepository = accountRepository;
        _locationRepository = locationRepository;
    }

    public async Task<Animal?> GetByIdAsync(long animalId)
    {
        return await _animalRepository.FindFirstOrDefaultAsync(x => x.Id == animalId);
    }

    public async Task<Animal> CreateAsync(CreateAnimalModel model)
    {
        var animalsTypes = await _animalTypeRepository.FindAllAsync(x => model.AnimalTypes.Contains(x.Id));
        if (animalsTypes.Count != model.AnimalTypes.Count) throw new AnimalCrateException("One of accepted animals types does not exists", HttpStatusCode.NotFound);

        var accountExists = await _accountRepository.ExistsAsync(x => x.Id == model.ChipperId);
        if (accountExists == false) throw new AnimalCrateException($"Account with id {model.ChipperId} does not exists", HttpStatusCode.NotFound);

        var locationExists = await _locationRepository.ExistsAsync(x => x.Id == model.ChippingLocationId);
        if (locationExists == false) throw new AnimalCrateException($"Location with id {model.ChippingLocationId} does not exists", HttpStatusCode.NotFound);

        var newAnimal = new Animal
        {
            WeightKilograms = model.Weight,
            HeightMeters = model.Height,
            LengthMeters = model.Length,
            Gender = model.Gender,
            LifeStatus = LifeStatus.Alive,
            ChippingDateTime = DateTime.UtcNow,
            ChipperId = model.ChipperId,
            ChippingLocationId = model.ChippingLocationId,
            DeathDateTime = null,
            AnimalTypes = new List<AnimalType>()
        };
        newAnimal.AnimalTypes.AddRange(animalsTypes);

        await _animalRepository.InsertAsync(newAnimal);
        return newAnimal;
    }

    public async Task<IEnumerable<Animal>> SearchAsync(SearchAnimalModel model)
    {
        var animals = _animalRepository.AsQueryable();

        if (string.IsNullOrWhiteSpace(model.Gender) == false) animals = animals.Where(x => x.Gender == model.Gender);
        if (string.IsNullOrWhiteSpace(model.LifeStatus) == false) animals = animals.Where(x => x.LifeStatus == model.LifeStatus);
        if (model.ChipperId is not null) animals = animals.Where(x => x.ChipperId == model.ChipperId);
        if (model.ChippingLocationId is not null) animals = animals.Where(x => x.ChippingLocationId == model.ChippingLocationId);
        if (model.StartDateTime is not null) animals = animals.Where(x => x.ChippingDateTime >= model.StartDateTime);
        if (model.EndDateTime is not null) animals = animals.Where(x => x.ChippingDateTime <= model.EndDateTime);

        return await animals
            .OrderBy(x => x.Id)
            .Skip(model.From)
            .Take(model.Size)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Animal> UpdateAsync(UpdateAnimalModel model)
    {
        var animal = await _animalRepository.FindFirstOrDefaultAsync(x => x.Id == model.Id);
        if (animal == null) throw new AnimalUpdateException($"Animal with id {model.Id} does not exists", HttpStatusCode.NotFound);

        var accountExists = await _accountRepository.ExistsAsync(x => x.Id == model.ChipperId);
        if (accountExists == false) throw new AnimalCrateException($"Account with id {model.ChipperId} does not exists", HttpStatusCode.NotFound);

        var locationExists = await _locationRepository.ExistsAsync(x => x.Id == model.ChippingLocationId);
        if (locationExists == false) throw new AnimalCrateException($"Location with id {model.ChippingLocationId} does not exists", HttpStatusCode.NotFound);

        // todo: validate new chipping location must be not equal first chipping location

        animal.ChipperId = model.ChipperId;
        animal.ChippingLocationId = model.ChippingLocationId;
        animal.Gender = model.Gender;
        animal.LifeStatus = model.LifeStatus;
        animal.WeightKilograms = model.Weight;
        animal.HeightMeters = model.Height;
        animal.LengthMeters = model.Length;

        return await _animalRepository.UpdateAsync(animal);
    }

    public async Task<Animal> AttachAnimalTypeAsync(long animalId, long animalTypeId)
    {
        var animal = await _animalRepository.FirstOrDefaultWithAnimalsTypesAsync(x => x.Id == animalId);
        if (animal == null) throw new AnimalAttachAnimalTypeException($"Animal with id {animalId} does not exists", HttpStatusCode.NotFound);

        if (animal.AnimalTypes.Any(x => x.Id == animalTypeId)) throw new AnimalAttachAnimalTypeException($"Animal with id {animalId} already has type with id {animalTypeId}", HttpStatusCode.Conflict);

        var animalType = await _animalTypeRepository.FindFirstOrDefaultAsync(x => x.Id == animalTypeId);
        if (animalType == null) throw new AnimalAttachAnimalTypeException($"Animal type with id {animalTypeId} does not exists", HttpStatusCode.NotFound);

        animal.AnimalTypes.Add(animalType);
        return await _animalRepository.UpdateAsync(animal);
    }

    public async Task<Animal> DeleteAnimalTypeAsync(long animalId, long animalTypeId)
    {
        var animal = await _animalRepository.FirstOrDefaultWithAnimalsTypesAsync(x => x.Id == animalId);
        if (animal == null) throw new AnimalDeleteAnimalTypeException($"Animal with id {animalId} does not exists", HttpStatusCode.NotFound);

        if (animal.AnimalTypes.Any(x => x.Id == animalTypeId) == false) throw new AnimalDeleteAnimalTypeException($"Animal with id {animalId} already has type with id {animalTypeId}", HttpStatusCode.NotFound);
        if (animal.AnimalTypes.Count == 1) throw new AnimalDeleteAnimalTypeException($"Animal with id {animalId} has only one type", HttpStatusCode.BadRequest);

        var animalType = await _animalTypeRepository.FindFirstOrDefaultAsync(x => x.Id == animalTypeId);
        if (animalType == null) throw new AnimalDeleteAnimalTypeException($"Animal type with id {animalTypeId} does not exists", HttpStatusCode.NotFound);

        animal.AnimalTypes.RemoveAll(x => x.Id == animalType.Id);
        return await _animalRepository.UpdateAsync(animal);
    }
}