using AnimalChipization.Api.Contracts.HealthCheck.Get;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("health-check")]
public class HealthCheckController : ApiControllerBase
{
    private readonly ILogger<HealthCheckController> _logger;

    public HealthCheckController(ILogger<HealthCheckController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(HealthCheckGetResponse), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var response = new HealthCheckGetResponse { Message = "Ok" };
        _logger.LogInformation("Health check perform occurred");
        return Ok(response);
    }
}