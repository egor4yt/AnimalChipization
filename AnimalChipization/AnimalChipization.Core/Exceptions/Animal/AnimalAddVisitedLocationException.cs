using System.Net;

namespace AnimalChipization.Core.Exceptions.Animal;

public class AnimalAddVisitedLocationException : Exception, IApiException
{
    public AnimalAddVisitedLocationException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCode HttpStatusCode { get; }

    public string ApiMessage { get; }
}