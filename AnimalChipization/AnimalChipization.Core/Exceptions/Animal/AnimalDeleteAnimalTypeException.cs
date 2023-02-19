using System.Net;

namespace AnimalChipization.Core.Exceptions.Animal;

public class AnimalDeleteAnimalTypeException : Exception, IApiException
{
    public AnimalDeleteAnimalTypeException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}