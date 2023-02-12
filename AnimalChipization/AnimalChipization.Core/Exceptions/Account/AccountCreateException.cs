using System.Net;

namespace AnimalChipization.Core.Exceptions.Account;

public class AccountCreateException : Exception, IApiException
{
    public AccountCreateException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}