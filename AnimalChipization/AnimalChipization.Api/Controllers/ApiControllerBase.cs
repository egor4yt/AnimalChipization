using System.Net;
using System.Runtime.InteropServices.ComTypes;
using AnimalChipization.Core.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
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

        if (exception is AggregateException aggregateException)
        {
            foreach (var innerException in aggregateException.InnerExceptions)
            {
                Logger.LogError(innerException.Message, innerException);
            }
        }
        else
        {
            Logger.LogError(exception.Message, exception);
        }
        
        return exception switch
        {
            IApiException apiException => StatusCode((int)apiException.HttpStatusCode, apiException.ApiMessage),
            _ => StatusCode((int)HttpStatusCode.BadRequest, "Something went wrong")
        };
    }
}