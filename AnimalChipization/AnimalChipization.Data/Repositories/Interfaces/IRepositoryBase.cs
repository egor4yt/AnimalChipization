using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    Task<List<TEntity>> GetAllAsync();
    IQueryable<TEntity> AsQueryable();
    Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> match);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> match);
    Task InsertAsync(TEntity entity);
}