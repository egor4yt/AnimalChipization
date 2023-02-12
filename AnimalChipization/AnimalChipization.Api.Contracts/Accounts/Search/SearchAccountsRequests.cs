using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.Accounts.Search;

public class SearchAccountsRequests
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    [Range(0, int.MaxValue)] public int From { get; set; } = 0;

    [Range(1, int.MaxValue)] public int Size { get; set; } = 10;
}