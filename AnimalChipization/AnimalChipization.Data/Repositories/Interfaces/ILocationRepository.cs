using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface ILocationRepository : IRepositoryBase<Location>
{
    Task<Location?> FirstOrDefaultWithAnimalsAsync(Expression<Func<Location, bool>> match);
}