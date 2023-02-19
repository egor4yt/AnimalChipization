using System.Net;

namespace AnimalChipization.Core.Exceptions.AnimalVisitedLocation;

public class AnimalVisitedLocationUpdateException : Exception, IApiException
{
    public AnimalVisitedLocationUpdateException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}