using System.Net;

namespace AnimalChipization.Core.Exceptions.Animal;

public class AnimalAttachAnimalTypeException : Exception, IApiException
{
    public AnimalAttachAnimalTypeException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}