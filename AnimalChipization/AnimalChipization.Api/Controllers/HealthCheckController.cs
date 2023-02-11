using AnimalChipization.Api.Contracts.HealthCheck.Get;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("health-check")]
public class HealthCheckController : ApiControllerBase
{
    public HealthCheckController(ILogger<HealthCheckController> logger) : base(logger)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(HealthCheckGetResponse), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var response = new HealthCheckGetResponse { Message = "Ok" };
        Logger.LogInformation("Health check perform occurred");
        return Ok(response);
    }
}