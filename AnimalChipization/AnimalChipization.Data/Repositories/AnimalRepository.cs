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

    public async Task<Animal?> FirstOrDefaultWithAnimalsTypesAsync(Expression<Func<Animal, bool>> match)
    {
        return await DbSet.Include(x => x.AnimalTypes).FirstOrDefaultAsync(match);
    }
}