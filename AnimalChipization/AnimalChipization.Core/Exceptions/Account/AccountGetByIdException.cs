using System.Net;

namespace AnimalChipization.Core.Exceptions.Account;

public class AccountGetByIdException : Exception, IApiException
{
    public AccountGetByIdException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}