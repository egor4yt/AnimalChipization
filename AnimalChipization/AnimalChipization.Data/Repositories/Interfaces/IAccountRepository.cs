using System.Linq.Expressions;
using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAccountRepository : IRepositoryBase<Account>
{
    Task<Account?> FirstOrDefaultWithAnimalsAsync(Expression<Func<Account, bool>> match);
}