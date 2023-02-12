using AnimalChipization.Data.Entities;

namespace AnimalChipization.Services.Services.Interfaces;

public interface IAccountService
{
    Task RegisterAsync(Account account);
}