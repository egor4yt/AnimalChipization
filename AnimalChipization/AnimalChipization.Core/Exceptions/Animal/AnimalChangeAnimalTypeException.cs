using System.Net;

namespace AnimalChipization.Core.Exceptions.Animal;

public class AnimalChangeAnimalTypeException : Exception, IApiException
{
    public AnimalChangeAnimalTypeException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}