using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAccountRepository : IRepositoryBase<Account>
{
    // Can bee extended by any additional methods that do not present in RepositoryBase
}