using System.Linq.Expressions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    private readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> DbSet;

    protected RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return DbSet.AsQueryable();
    }

    public virtual async Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> match)
    {
        return await DbSet.FirstOrDefaultAsync(match);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await DbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> match)
    {
        return await DbSet.AnyAsync(match);
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
    {
        if (DbSet.Entry(entityToUpdate).State == EntityState.Detached) DbSet.Attach(entityToUpdate);

        DbSet.Entry(entityToUpdate).State = EntityState.Modified;
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        await _context.SaveChangesAsync();

        return entityToUpdate;
    }

    public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
    {
        return await DbSet.Where(match).ToListAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entity)
    {
        DbSet.RemoveRange(entity);
        await _context.SaveChangesAsync();
    }
}