using System.Net;
using AnimalChipization.Core.Exceptions.Account;
using AnimalChipization.Core.Helpers;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.Account;
using AnimalChipization.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Services.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task RegisterAsync(Account account)
    {
        if (account == null) throw new AccountRegisterException("Account was null", HttpStatusCode.BadRequest);
        var accountExist = await _accountRepository.ExistsAsync(x => x.Email == account.Email);
        if (accountExist)
            throw new AccountRegisterException($"Account with email {account.Email} already exists",
                HttpStatusCode.Conflict);

        await _accountRepository.InsertAsync(account);
    }

    public async Task<Account?> GetByIdAsync(int accountId)
    {
        if (accountId <= 0) throw new AccountRegisterException("Invalid account id", HttpStatusCode.BadRequest);
        return await _accountRepository.FindFirstOrDefaultAsync(x => x.Id == accountId);
    }

    public async Task<Account?> AuthenticateAsync(string email, string password)
    {
        var passwordHash = SecurityHelper.ComputeSha256Hash(password);
        return await _accountRepository.FindFirstOrDefaultAsync(x =>
            x.Email == email && x.PasswordHash == passwordHash);
    }

    public async Task<IEnumerable<Account>> SearchAsync(SearchAccountModel model)
    {
        var accounts = _accountRepository.AsQueryable();
        
        if (string.IsNullOrWhiteSpace(model.FirstName) == false)
            accounts = accounts.Where(x => x.FirstName.ToLower().Contains(model.FirstName.ToLower()));

        if (string.IsNullOrWhiteSpace(model.LastName) == false)
            accounts = accounts.Where(x => x.LastName.ToLower().Contains(model.LastName.ToLower()));

        if (string.IsNullOrWhiteSpace(model.Email) == false)
            accounts = accounts.Where(x => x.Email.ToLower().Contains(model.Email.ToLower()));

        return await accounts.Skip(model.From).Take(model.Size).AsNoTracking().ToListAsync();
    }
}