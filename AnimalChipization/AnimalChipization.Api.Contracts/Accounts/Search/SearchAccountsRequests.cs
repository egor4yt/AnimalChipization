using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.Paging;

namespace AnimalChipization.Api.Contracts.Accounts.Search;

public class SearchAccountsRequests : PagingSettings
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}