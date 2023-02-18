using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;

namespace AnimalChipization.Data.Repositories;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Can bee extended by any additional methods that do not present in RepositoryBase
}