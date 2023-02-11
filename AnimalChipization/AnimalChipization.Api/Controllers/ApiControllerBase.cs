using System.Net;
using AnimalChipization.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult ExceptionResult(Exception exception)
    {
        return exception switch
        {
            IApiException apiException => StatusCode((int)HttpStatusCode.BadRequest, apiException.ApiMessage),
            _ => StatusCode((int)HttpStatusCode.BadRequest, "Something went wrong")
        };
    }
}