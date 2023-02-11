using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> FindFirstOrDefault(Expression<Func<TEntity, bool>> match);
    Task InsertAsync(TEntity entity);
}