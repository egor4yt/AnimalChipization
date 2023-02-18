using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAnimalTypeRepository : IRepositoryBase<AnimalType>
{
    // Can bee extended by any additional methods that do not present in IRepositoryBase
}