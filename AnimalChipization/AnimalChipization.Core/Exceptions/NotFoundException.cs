using System.Net;

namespace AnimalChipization.Core.Exceptions;

public class NotFoundException : Exception, IApiException
{
    public NotFoundException(string apiMessage)
    {
        ApiMessage = apiMessage;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;
}