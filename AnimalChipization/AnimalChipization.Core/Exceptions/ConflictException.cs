using System.Net;

namespace AnimalChipization.Core.Exceptions;

public class ConflictException : ApiException
{
    public ConflictException(string apiMessage) : base(apiMessage, HttpStatusCode.Conflict)
    {
    }
}