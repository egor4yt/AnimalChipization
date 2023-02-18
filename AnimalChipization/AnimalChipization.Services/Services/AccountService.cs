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

        account.Email = account.Email.ToLower();
        var accountExist = await _accountRepository.ExistsAsync(x => x.Email.ToLower() == account.Email);
        if (accountExist) throw new AccountRegisterException($"Account with email {account.Email} already exists", HttpStatusCode.Conflict);

        await _accountRepository.InsertAsync(account);
    }

    public async Task<Account?> GetByIdAsync(int accountId)
    {
        return await _accountRepository.FindFirstOrDefaultAsync(x => x.Id == accountId);
    }

    public async Task<Account?> AuthenticateAsync(string email, string password)
    {
        var passwordHash = SecurityHelper.ComputeSha256Hash(password);
        return await _accountRepository.FindFirstOrDefaultAsync(x =>
            x.Email.ToLower() == email.ToLower()
            && x.PasswordHash == passwordHash
        );
    }

    public async Task<IEnumerable<Account>> SearchAsync(SearchAccountModel model)
    {
        var accounts = _accountRepository.AsQueryable();

        if (string.IsNullOrWhiteSpace(model.FirstName) == false) accounts = accounts.Where(x => EF.Functions.Like(x.FirstName.ToLower(), $"%{model.FirstName.ToLower()}%"));
        if (string.IsNullOrWhiteSpace(model.LastName) == false) accounts = accounts.Where(x => EF.Functions.Like(x.LastName.ToLower(), $"%{model.LastName.ToLower()}%"));
        if (string.IsNullOrWhiteSpace(model.Email) == false) accounts = accounts.Where(x => EF.Functions.Like(x.Email.ToLower(), $"%{model.Email.ToLower()}%"));

        return await accounts
            .OrderBy(x => x.Id)
            .Skip(model.From)
            .Take(model.Size)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Account> UpdateAsync(UpdateAccountModel model)
    {
        model.Email = model.Email.ToLower().Trim();
        
        var account = await _accountRepository.FindFirstOrDefaultAsync(x => x.Id == model.Id);
        if (account == null) throw new AccountUpdateException($"Account with id {model.Id} does not exists", HttpStatusCode.Forbidden);

        var accountExist = await _accountRepository.ExistsAsync(x => x.Email.ToLower() == account.Email.ToLower() && x.Id != model.Id);
        if (accountExist) throw new AccountRegisterException($"Account with email {model.Email} already exists", HttpStatusCode.Conflict);

        account.FirstName = model.FirstName;
        account.LastName = model.LastName;
        account.Email = model.Email;
        account.PasswordHash = SecurityHelper.ComputeSha256Hash(model.Password);

        return await _accountRepository.UpdateAsync(account);
    }
}