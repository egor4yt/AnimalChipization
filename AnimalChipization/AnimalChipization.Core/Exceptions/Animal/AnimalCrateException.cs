using System.Net;

namespace AnimalChipization.Core.Exceptions.Animal;

public class AnimalCrateException : Exception, IApiException
{
    public AnimalCrateException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}