using System.Net;
using AnimalChipization.Core.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ILogger<ApiControllerBase> Logger;
    protected readonly IMapper Mapper;


    protected ApiControllerBase(ILogger<ApiControllerBase> logger, IMapper mapper)
    {
        Logger = logger;
        Mapper = mapper;
    }

    protected IActionResult ExceptionResult(Exception exception)
    {
        Logger.LogError(exception.Message, exception);
        return exception switch
        {
            IApiException apiException => StatusCode((int)apiException.HttpStatusCode, apiException.ApiMessage),
            _ => StatusCode((int)HttpStatusCode.BadRequest, "Something went wrong")
        };
    }
}