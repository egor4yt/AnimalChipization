using AnimalChipization.Data.Entities;

namespace AnimalChipization.Data.Repositories.Interfaces;

public interface IAccountRepository : IRepositoryBase<Account>
{
    Task<bool> IsExistByEmailAsync(string email);
    Task<Account?> AuthenticateUserAsync(string email, string password);
}