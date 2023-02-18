using System.Net;

namespace AnimalChipization.Core.Exceptions;

public interface IApiException
{
    string ApiMessage { get; }
    HttpStatusCode HttpStatusCode { get; }
}