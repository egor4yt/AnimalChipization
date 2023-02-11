namespace AnimalChipization.Core.Exceptions.Account;

public class AccountCreateExceptions : Exception, IApiException
{
    public AccountCreateExceptions(string apiMessage)
    {
        ApiMessage = apiMessage;
    }

    public string ApiMessage { get; }
}