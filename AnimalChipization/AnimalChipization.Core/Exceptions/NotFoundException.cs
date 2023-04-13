using System.Net;

namespace AnimalChipization.Core.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string apiMessage) : base(apiMessage, HttpStatusCode.NotFound)
    {
    }
}