using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface ILocationRepository : IRepositoryBase<Location>
{
    // Can bee extended by any additional methods that do not present in IRepositoryBase
}