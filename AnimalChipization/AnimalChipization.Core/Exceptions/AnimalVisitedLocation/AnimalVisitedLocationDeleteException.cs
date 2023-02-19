using System.Net;

namespace AnimalChipization.Core.Exceptions.AnimalVisitedLocation;

public class AnimalVisitedLocationDeleteException : Exception, IApiException
{
    public AnimalVisitedLocationDeleteException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}