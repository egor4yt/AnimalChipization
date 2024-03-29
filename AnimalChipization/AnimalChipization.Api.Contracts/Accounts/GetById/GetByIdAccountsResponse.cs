namespace AnimalChipization.Api.Contracts.Accounts.GetById;

public class GetByIdAccountsResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}