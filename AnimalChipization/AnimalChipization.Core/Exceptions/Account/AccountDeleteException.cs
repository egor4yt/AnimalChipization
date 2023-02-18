using System.Net;

namespace AnimalChipization.Core.Exceptions.Account;

public class AccountDeleteException: Exception, IApiException
{
    public AccountDeleteException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}