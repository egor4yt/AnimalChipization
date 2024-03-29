using AnimalChipization.Core.Exceptions;
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
        return await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == animalId);
    }

    public async Task<Animal> CreateAsync(CreateAnimalModel model)
    {
        var animalsTypes = await _animalTypeRepository.FindAllAsync(x => model.AnimalTypes.Contains(x.Id));
        if (animalsTypes.Count != model.AnimalTypes.Count) throw new NotFoundException("One of accepted animals types does not exists");

        var accountExists = await _accountRepository.ExistsAsync(x => x.Id == model.ChipperId);
        if (accountExists == false) throw new NotFoundException($"Account with id {model.ChipperId} does not exists");

        var locationExists = await _locationRepository.ExistsAsync(x => x.Id == model.ChippingLocationId);
        if (locationExists == false) throw new NotFoundException($"Location with id {model.ChippingLocationId} does not exists");

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
            .Include(x => x.AnimalTypes)
            .Include(x => x.AnimalVisitedLocations)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Animal> UpdateAsync(UpdateAnimalModel model)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == model.Id);
        if (animal == null) throw new NotFoundException($"Animal with id {model.Id} does not exists");

        var accountExists = await _accountRepository.ExistsAsync(x => x.Id == model.ChipperId);
        if (accountExists == false) throw new NotFoundException($"Account with id {model.ChipperId} does not exists");

        var locationExists = await _locationRepository.ExistsAsync(x => x.Id == model.ChippingLocationId);
        if (locationExists == false) throw new NotFoundException($"Location with id {model.ChippingLocationId} does not exists");

        var firstVisitedLocation = animal.AnimalVisitedLocations.FirstOrDefault();
        if (firstVisitedLocation != null && firstVisitedLocation.LocationId == model.ChippingLocationId) throw new BadRequestException($"Location with id {model.ChippingLocationId} is first visited location");

        animal.ChipperId = model.ChipperId;
        animal.ChippingLocationId = model.ChippingLocationId;
        animal.Gender = model.Gender;
        animal.LifeStatus = model.LifeStatus;
        animal.WeightKilograms = model.Weight;
        animal.HeightMeters = model.Height;
        animal.LengthMeters = model.Length;
        animal.DeathDateTime = model.LifeStatus == LifeStatus.Dead ? DateTime.UtcNow : null;

        return await _animalRepository.UpdateAsync(animal);
    }

    public async Task<Animal> AttachAnimalTypeAsync(long animalId, long animalTypeId)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == animalId);
        if (animal == null) throw new NotFoundException($"Animal with id {animalId} does not exists");

        if (animal.AnimalTypes.Any(x => x.Id == animalTypeId)) throw new ConflictException($"Animal with id {animalId} already has type with id {animalTypeId}");

        var animalType = await _animalTypeRepository.FindFirstOrDefaultAsync(x => x.Id == animalTypeId);
        if (animalType == null) throw new NotFoundException($"Animal type with id {animalTypeId} does not exists");

        animal.AnimalTypes.Add(animalType);
        return await _animalRepository.UpdateAsync(animal);
    }

    public async Task<Animal> ChangeAnimalTypeAsync(ChangeAnimalTypeAnimalModel model)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == model.AnimalId);
        if (animal == null) throw new NotFoundException($"Animal with id {model.AnimalId} does not exists");

        var animalTypes = await _animalTypeRepository.FindAllAsync(x => x.Id == model.OldTypeId || x.Id == model.NewTypeId);

        var oldType = animalTypes.FirstOrDefault(x => x.Id == model.OldTypeId);
        if (oldType == null) throw new NotFoundException($"Animal type with id {model.OldTypeId} does not exists");

        var newType = animalTypes.FirstOrDefault(x => x.Id == model.NewTypeId);
        if (newType == null) throw new NotFoundException($"Animal type with id {model.NewTypeId} does not exists");

        if (animal.AnimalTypes.Any(x => x.Id == oldType.Id) == false) throw new NotFoundException($"Animal with id {model.AnimalId} already has type with id {oldType.Id}");
        if (animal.AnimalTypes.Any(x => x.Id == newType.Id)) throw new ConflictException($"Animal with id {model.AnimalId} already has type with id {newType.Id}");

        animal.AnimalTypes.RemoveAll(x => x.Id == oldType.Id);
        animal.AnimalTypes.Add(newType);

        return await _animalRepository.UpdateAsync(animal);
    }

    public async Task<Animal> DetachAnimalTypeAsync(long animalId, long animalTypeId)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == animalId);
        if (animal == null) throw new NotFoundException($"Animal with id {animalId} does not exists");

        if (animal.AnimalTypes.Any(x => x.Id == animalTypeId) == false) throw new NotFoundException($"Animal with id {animalId} has not animal type with id {animalTypeId}");
        if (animal.AnimalTypes.Count == 1) throw new BadRequestException($"Animal with id {animalId} has only one type");

        var animalType = await _animalTypeRepository.FindFirstOrDefaultAsync(x => x.Id == animalTypeId);
        if (animalType == null) throw new NotFoundException($"Animal type with id {animalTypeId} does not exists");

        animal.AnimalTypes.RemoveAll(x => x.Id == animalType.Id);
        return await _animalRepository.UpdateAsync(animal);
    }

    public async Task DeleteAsync(long animalId)
    {
        var animal = await _animalRepository.FirstOrDefaultFullAsync(x => x.Id == animalId);
        if (animal is null) throw new NotFoundException($"Animal with id {animalId} does not exists");

        var lastVisitedLocation = animal.AnimalVisitedLocations.MaxBy(x => x.CreatedAt);
        if (lastVisitedLocation is not null && lastVisitedLocation.LocationId != animal.ChippingLocationId) throw new BadRequestException("Animal is not in the chipping location");

        await _animalRepository.DeleteAsync(animal);
    }
}