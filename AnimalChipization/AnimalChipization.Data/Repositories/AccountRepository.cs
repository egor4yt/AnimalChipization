using System.Linq.Expressions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Account?> FirstOrDefaultWithAnimalsAsync(Expression<Func<Account, bool>> match)
    {
        return await DbSet.Include(x=>x.Animals).FirstOrDefaultAsync(match);
    }
}