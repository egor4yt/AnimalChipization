using AnimalChipization.Core.Exceptions;
using AnimalChipization.Core.Helpers;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
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
        if (account == null) throw new BadRequestException("Account was null");

        account.Email = account.Email.ToLower();
        account.Role = AccountRole.User;
        var accountExist = await _accountRepository.ExistsAsync(x => x.Email.ToLower() == account.Email);
        if (accountExist) throw new ConflictException($"Account with email {account.Email} already exists");

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
        if (account == null) throw new ForbiddenException($"Account with id {model.Id} does not exists");

        var accountExist = await _accountRepository.ExistsAsync(x => x.Email.ToLower() == account.Email.ToLower() && x.Id != model.Id);
        if (accountExist) throw new ConflictException($"Account with email {model.Email} already exists");

        account.FirstName = model.FirstName;
        account.LastName = model.LastName;
        account.Email = model.Email;
        account.PasswordHash = SecurityHelper.ComputeSha256Hash(model.Password);

        return await _accountRepository.UpdateAsync(account);
    }

    public async Task DeleteAsync(int accountId)
    {
        var account = await _accountRepository.FirstOrDefaultWithAnimalsAsync(x => x.Id == accountId);
        if (account is null) throw new ForbiddenException($"Account with id {accountId} does not exists");
        if (account.Animals.Any()) throw new BadRequestException($"Account with id {accountId} has relations with animals");

        await _accountRepository.DeleteAsync(account);
    }
}