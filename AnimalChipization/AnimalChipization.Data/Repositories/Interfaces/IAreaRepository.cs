using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAreaRepository : IRepositoryBase<Area>
{
    Task<Area?> FirstOrDefaultFullAsync(Expression<Func<Area, bool>> match);
}