using System.Net;

namespace AnimalChipization.Core.Exceptions.Animal;

public class AnimalDeleteException : Exception, IApiException
{
    public AnimalDeleteException(string apiMessage, HttpStatusCode httpStatusCode)
    {
        ApiMessage = apiMessage;
        HttpStatusCode = httpStatusCode;
    }

    public string ApiMessage { get; }
    public HttpStatusCode HttpStatusCode { get; }
}