using System.Linq.Expressions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public class AnimalRepository : RepositoryBase<Animal>, IAnimalRepository
{
    public AnimalRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Animal?> FirstOrDefaultFullAsync(Expression<Func<Animal, bool>> match)
    {
        return await DbSet
            .Include(l => l
                .AnimalVisitedLocations.OrderBy(x => x.CreatedAt))
            .Include(x => x.AnimalTypes)
            .FirstOrDefaultAsync(match);
    }

    public async Task<List<Animal>> FindAllFullAsync(Expression<Func<Animal, bool>> match)
    {
        return await DbSet
            .Include(x => x.AnimalTypes)
            .Include(x => x.ChippingLocation)
            .Include(l => l
                .AnimalVisitedLocations.OrderBy(x => x.CreatedAt))
            .ThenInclude(x => x.Location)
            .Where(match)
            .ToListAsync();
    }
}