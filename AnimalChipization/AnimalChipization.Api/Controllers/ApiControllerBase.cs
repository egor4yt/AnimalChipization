using System.Net;
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
        if (exception is AggregateException aggregateEx)
            foreach (var innerException in aggregateEx.InnerExceptions)
                Logger.LogError(innerException.Message, innerException);
        else
            Logger.LogError(exception.Message, exception);

        return exception switch
        {
            IApiException apiException => StatusCode((int)apiException.HttpStatusCode, apiException.ApiMessage),
            _ => StatusCode((int)HttpStatusCode.BadRequest, $"Something went wrong: {GetErrorMessage(exception)}")
        };
    }

    private string GetErrorMessage(Exception exception)
    {
        if (exception is AggregateException aggregateEx)
        {
            var msg = "";
            foreach (var innerException in aggregateEx.InnerExceptions)
                msg += $"{innerException.Message}\nStack trace: {innerException.StackTrace}\n";
            return msg;
        }

        return $"{exception.Message}\nStack trace: {exception.StackTrace}";
    }
}