using AnimalChipization.Data.Entities;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAccountService
{
    Task RegisterAsync(Account account);
    Task<Account?> GetByIdAsync(int accountId);
    Task<Account?> AuthenticateAsync(string email, string password);
    
}