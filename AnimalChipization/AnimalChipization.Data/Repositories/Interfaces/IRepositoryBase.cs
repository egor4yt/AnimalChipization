using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    IQueryable<TEntity> AsQueryable();
    Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> match);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> match);
    Task InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entityToUpdate);
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entity);
}