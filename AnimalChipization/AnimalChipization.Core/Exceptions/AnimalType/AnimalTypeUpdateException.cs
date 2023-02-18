using System.Net;

namespace AnimalChipization.Core.Exceptions.AnimalType;

public class AnimalTypeUpdateException : Exception, IApiException
{
    public AnimalTypeUpdateException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}