using System.Net;

namespace AnimalChipization.Core.Exceptions.Animal;

public class AnimalUpdateException : Exception, IApiException
{
    public AnimalUpdateException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}