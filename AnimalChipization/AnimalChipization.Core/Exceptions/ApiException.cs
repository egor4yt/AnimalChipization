using System.Net;

namespace AnimalChipization.Core.Exceptions;

public abstract class ApiException : Exception, IApiException
{
    protected ApiException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }
    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}