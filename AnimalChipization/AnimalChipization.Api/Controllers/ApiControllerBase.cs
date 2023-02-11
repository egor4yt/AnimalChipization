using System.Net;
using AnimalChipization.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ILogger<ApiControllerBase> Logger;

    protected ApiControllerBase(ILogger<ApiControllerBase> logger)
    {
        Logger = logger;
    }

    protected IActionResult ExceptionResult(Exception exception)
    {
        Logger.LogError(exception.Message, exception);
        return exception switch
        {
            IApiException apiException => StatusCode((int)HttpStatusCode.BadRequest, apiException.ApiMessage),
            _ => StatusCode((int)HttpStatusCode.BadRequest, "Something went wrong")
        };
    }
}