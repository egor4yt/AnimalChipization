using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAnimalRepository : IRepositoryBase<Animal>
{
    Task<Animal?> FirstOrDefaultFullAsync(Expression<Func<Animal, bool>> match);
    Task<List<Animal>> FindAllFullAsync(Expression<Func<Animal, bool>> match);
}