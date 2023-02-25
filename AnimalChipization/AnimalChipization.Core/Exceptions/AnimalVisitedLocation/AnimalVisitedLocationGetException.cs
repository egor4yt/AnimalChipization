using System.Net;

namespace AnimalChipization.Core.Exceptions.AnimalVisitedLocation;

public class AnimalVisitedLocationGetException: Exception, IApiException
{
    public AnimalVisitedLocationGetException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}