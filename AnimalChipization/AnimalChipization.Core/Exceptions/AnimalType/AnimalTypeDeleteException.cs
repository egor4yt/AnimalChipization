using System.Net;

namespace AnimalChipization.Core.Exceptions.AnimalType;

public class AnimalTypeDeleteException : Exception, IApiException
{
    public AnimalTypeDeleteException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCode HttpStatusCode { get; }

    public string ApiMessage { get; }
}