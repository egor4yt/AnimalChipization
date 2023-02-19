using System.Net;

namespace AnimalChipization.Core.Exceptions.Animal;

public class AnimalDeleteVisitedLocationException : Exception, IApiException
{
    public AnimalDeleteVisitedLocationException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCode HttpStatusCode { get; }

    public string ApiMessage { get; }
}