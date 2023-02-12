using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAccountRepository : IRepositoryBase<Account>
{
    Task<bool> IsExistByEmailAsync(string email);
}