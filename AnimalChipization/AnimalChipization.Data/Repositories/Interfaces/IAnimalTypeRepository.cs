using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAnimalTypeRepository : IRepositoryBase<AnimalType>
{
    Task<AnimalType?> FirstOrDefaultWithAnimalsAsync(Expression<Func<AnimalType, bool>> match);
}