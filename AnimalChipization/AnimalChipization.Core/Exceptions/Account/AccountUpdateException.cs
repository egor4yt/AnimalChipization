using System.Net;

namespace AnimalChipization.Core.Exceptions.Account;

public class AccountUpdateException : Exception, IApiException
{
    public AccountUpdateException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}