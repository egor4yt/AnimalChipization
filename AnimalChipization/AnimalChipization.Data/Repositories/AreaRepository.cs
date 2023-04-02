using System.Linq.Expressions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public class AreaRepository : RepositoryBase<Area>, IAreaRepository
{
    public AreaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Area?> FirstOrDefaultFullAsync(Expression<Func<Area, bool>> match)
    {
        return await DbSet
            .FirstOrDefaultAsync(match);
    }
}