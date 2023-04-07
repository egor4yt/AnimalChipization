using AnimalChipization.Api.Contracts.Analytics.Get;
using AnimalChipization.Services.Models.Analytics;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("areas/{areaId:long}/analytics")]
public class AnalyticsController : ApiControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(ILogger<AnalyticsController> logger, IMapper mapper, IAnalyticsService analyticsService) : base(logger, mapper)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAnalyticsResponse), StatusCodes.Status200OK)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Get([FromRoute] long areaId, [FromQuery] GetAnalyticsRequests request)
    {
        try
        {
            var model = Mapper.Map<AnalyzeAnimalsMovementModel>(request);
            model.AreaId = areaId;

            var serviceResponse = await _analyticsService.AnalyzeAnimalsMovement(model);
            var controllerResponse = Mapper.Map<GetAnalyticsResponse>(serviceResponse);
            return Ok(controllerResponse);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}