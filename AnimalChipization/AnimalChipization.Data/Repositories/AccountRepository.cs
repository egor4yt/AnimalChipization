using AnimalChipization.Core.Helpers;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Data.Repositories;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsExistByEmailAsync(string email)
    {
        return await _context.Accounts.AnyAsync(x => x.Email == email);
    }

    public async Task<Account?> AuthenticateUserAsync(string email, string password)
    {
        var passwordHash = SecurityHelper.ComputeSha256Hash(password);
        return await _context.Accounts.FirstOrDefaultAsync(x =>
            x.Email == email
            && x.PasswordHash == passwordHash);
    }
}