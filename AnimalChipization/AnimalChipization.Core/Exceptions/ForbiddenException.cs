using System.Net;

namespace AnimalChipization.Core.Exceptions;

public class ForbiddenException : Exception, IApiException
{
    public ForbiddenException(string apiMessage)
    {
        ApiMessage = apiMessage;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Forbidden;
}