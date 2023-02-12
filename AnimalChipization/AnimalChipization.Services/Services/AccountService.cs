using System.Net;
using AnimalChipization.Core.Exceptions.Account;
using AnimalChipization.Core.Helpers;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Services.Interfaces;

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
        var accountExist = await _accountRepository.Exists(x => x.Email == account.Email);
        if (accountExist)
            throw new AccountRegisterException($"Account with email {account.Email} already exists",
                HttpStatusCode.Conflict);

        await _accountRepository.InsertAsync(account);
    }

    public async Task<Account?> GetByIdAsync(int accountId)
    {
        if (accountId <= 0) throw new AccountRegisterException("Invalid account id", HttpStatusCode.BadRequest);
        return await _accountRepository.FindFirstOrDefault(x => x.Id == accountId);
    }

    public async Task<Account?> AuthenticateAsync(string email, string password)
    {
        var passwordHash = SecurityHelper.ComputeSha256Hash(password);
        return await _accountRepository.FindFirstOrDefault(x => x.Email == email && x.PasswordHash == passwordHash);
    }
}