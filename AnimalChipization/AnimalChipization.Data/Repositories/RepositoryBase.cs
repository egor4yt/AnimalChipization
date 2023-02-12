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

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbSet.AsQueryable();
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

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> match)
    {
        return await _dbSet.AnyAsync(match);
    }
}