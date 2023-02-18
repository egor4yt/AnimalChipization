using System.Net;

namespace AnimalChipization.Core.Exceptions.AnimalType;

public class AnimalTypeCreateException : Exception, IApiException
{
    public AnimalTypeCreateException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }
    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}