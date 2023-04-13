using System.Net;

namespace AnimalChipization.Core.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string apiMessage) : base(apiMessage, HttpStatusCode.BadRequest)
    {
    }
}