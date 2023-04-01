using System.Net;

namespace AnimalChipization.Core.Exceptions;

public class BadRequestException: Exception, IApiException
{
    public BadRequestException(string apiMessage)
    {
        ApiMessage = apiMessage;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}