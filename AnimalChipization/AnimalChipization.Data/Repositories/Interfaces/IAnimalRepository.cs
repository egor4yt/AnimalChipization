using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAnimalRepository : IRepositoryBase<Animal>
{
    Task<Animal?> FirstOrDefaultWithAnimalsTypesAsync(Expression<Func<Animal, bool>> match);
    Task<Animal?> FirstOrDefaultWithVisitedLocations(Expression<Func<Animal, bool>> match);
}