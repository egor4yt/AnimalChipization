using System.Net;
using AnimalChipization.Core.Exceptions.Animal;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.Animal;
using AnimalChipization.Services.Services.Interfaces;

namespace AnimalChipization.Services.Services;

public class AnimalService : IAnimalService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalTypeRepository _animalTypeRepository;

    public AnimalService(IAnimalTypeRepository animalTypeRepository, IAnimalRepository animalRepository, IAccountRepository accountRepository)
    {
        _animalTypeRepository = animalTypeRepository;
        _animalRepository = animalRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Animal> CreateAsync(CreateAnimalModel model)
    {
        var animalsTypes = await _animalTypeRepository.FindAllAsync(x => model.AnimalTypes.Contains(x.Id));
        if (animalsTypes.Count != model.AnimalTypes.Count) throw new AnimalCrateException("One of accepted animals types does not exists", HttpStatusCode.NotFound);

        var accountExists = await _accountRepository.ExistsAsync(x => x.Id == model.ChipperId);
        if (accountExists == false) throw new AnimalCrateException($"Account with id {model.ChipperId} does not exists", HttpStatusCode.NotFound);

        // todo: check if there is chipping location
        // var accountExists = await _accountRepository.ExistsAsync(x => x.Id == model.ChipperId);
        // if (accountExists == false) throw new AnimalCrateException($"Account with id {model.ChipperId} does not exists", HttpStatusCode.NotFound);

        var newAnimal = new Animal
        {
            WeightKilograms = model.Weight,
            HeightMeters = model.Height,
            LengthMeters = model.Length,
            Gender = model.Gender,
            LifeStatus = LifeStatus.Alive,
            ChippingDateTime = DateTime.UtcNow,
            ChipperId = model.ChipperId,
            DeathDateTime = null,
            AnimalTypes = new List<AnimalType>()
        };
        newAnimal.AnimalTypes.AddRange(animalsTypes);

        await _animalRepository.InsertAsync(newAnimal);
        return newAnimal;
    }
}