using System.Net;

namespace AnimalChipization.Core.Exceptions;

public class ForbiddenException : ApiException
{
    public ForbiddenException(string apiMessage) : base(apiMessage, HttpStatusCode.Forbidden)
    {
    }
}