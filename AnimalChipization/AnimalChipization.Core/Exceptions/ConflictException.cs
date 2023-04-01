using System.Net;

namespace AnimalChipization.Core.Exceptions;

public class ConflictException : Exception, IApiException
{
    public ConflictException(string apiMessage)
    {
        ApiMessage = apiMessage;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Conflict;
}