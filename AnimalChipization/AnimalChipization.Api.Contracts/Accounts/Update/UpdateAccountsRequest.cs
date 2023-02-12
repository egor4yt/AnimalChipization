using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.Accounts.Update;

public class UpdateAccountsRequest
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }
    [Required] public string Password { get; set; }
}