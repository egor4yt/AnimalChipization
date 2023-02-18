using System.Net;

namespace AnimalChipization.Core.Exceptions.Location;

public class LocationDeleteException: Exception, IApiException
{
    public LocationDeleteException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}