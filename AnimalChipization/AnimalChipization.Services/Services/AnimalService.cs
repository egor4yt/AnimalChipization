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

        // check if there is chipping location
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
}