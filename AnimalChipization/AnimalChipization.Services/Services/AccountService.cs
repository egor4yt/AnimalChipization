using System.Net;
using AnimalChipization.Core.Exceptions.Account;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Services.Interfaces;
using Npgsql;

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
        if (account == null) throw new AccountCreateException("Account was null", HttpStatusCode.BadRequest);
        var accountExist = await _accountRepository.IsExistByEmailAsync(account.Email);
        if (accountExist)throw new AccountCreateException($"Account with email {account.Email} already exists", HttpStatusCode.Conflict);
        
        await _accountRepository.InsertAsync(account);
    }
}