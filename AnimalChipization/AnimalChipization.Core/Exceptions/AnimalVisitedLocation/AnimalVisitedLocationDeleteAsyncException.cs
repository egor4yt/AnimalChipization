using System.Net;

namespace AnimalChipization.Core.Exceptions.AnimalVisitedLocation;

public class AnimalVisitedLocationDeleteAsyncException : Exception, IApiException
{
    public AnimalVisitedLocationDeleteAsyncException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}