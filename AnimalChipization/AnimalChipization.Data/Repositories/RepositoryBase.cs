using System.Linq.Expressions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> match)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(match);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> match)
    {
        return await _dbSet.AnyAsync(match);
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
    {
        if (_dbSet.Entry(entityToUpdate).State == EntityState.Detached) _dbSet.Attach(entityToUpdate);

        _dbSet.Entry(entityToUpdate).State = EntityState.Modified;
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        await _context.SaveChangesAsync();

        return entityToUpdate;
    }
}