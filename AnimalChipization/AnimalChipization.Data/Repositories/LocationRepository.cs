using System.Linq.Expressions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public class LocationRepository : RepositoryBase<Location>, ILocationRepository
{
    public LocationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Location?> FirstOrDefaultFullAsync(Expression<Func<Location, bool>> match)
    {
        return await DbSet
            .Include(x => x.Animals)
            .Include(x => x.AnimalsVisitedLocations)
            .FirstOrDefaultAsync(match);
    }
}