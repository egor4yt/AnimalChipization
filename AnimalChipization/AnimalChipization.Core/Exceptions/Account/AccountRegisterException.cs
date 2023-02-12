using System.Net;

namespace AnimalChipization.Core.Exceptions.Account;

public class AccountRegisterException : Exception, IApiException
{
    public AccountRegisterException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}