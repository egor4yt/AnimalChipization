using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.Account;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAccountService
{   
    Task RegisterAsync(Account account);
    Task<Account?> GetByIdAsync(int accountId);
    Task<Account?> AuthenticateAsync(string email, string password);
    Task<IEnumerable<Account>> SearchAsync(SearchAccountModel model);
    Task<Account> UpdateAsync(UpdateAccountModel model);
    Task DeleteAsync(int accountId);
}