using System.Net;

namespace AnimalChipization.Core.Exceptions.Location;

public class LocationUpdateException : Exception, IApiException
{
    public LocationUpdateException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}