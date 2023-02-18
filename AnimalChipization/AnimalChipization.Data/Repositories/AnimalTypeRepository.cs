using System.Linq.Expressions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public class AnimalTypeRepository : RepositoryBase<AnimalType>, IAnimalTypeRepository
{
    public AnimalTypeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<AnimalType?> FirstOrDefaultWithAnimalsAsync(Expression<Func<AnimalType, bool>> match)
    {
        return await DbSet.Include(x => x.Animals).FirstOrDefaultAsync(match);
    }
}