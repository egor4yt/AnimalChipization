using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.Validation;

namespace AnimalChipization.Api.Contracts.Accounts.Create;

public class CreateAccountsRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Role(false)]
    public string Role { get; set; }
}