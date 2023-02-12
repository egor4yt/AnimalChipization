using System.Net;

namespace AnimalChipization.Core.Exceptions.Location;

public class LocationGetByIdException : Exception, IApiException
{
    public LocationGetByIdException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}