using AnimalChipization.Api.Contracts.Accounts.GetById;
using AnimalChipization.Api.Contracts.Locations.Create;
using AnimalChipization.Api.Contracts.Locations.GetById;
using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("[controller]")]
public class LocationsController : ApiControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILogger<ApiControllerBase> logger, IMapper mapper, ILocationService locationService) :
        base(logger, mapper)
    {
        _locationService = locationService;
    }

    [HttpGet("{pointId:int}")]
    [ProducesResponseType(typeof(GetByIdLocationsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetById([FromRoute] int pointId)
    {
        try
        {
            var location = await _locationService.GetByIdAsync(pointId);
            if (location is null) return NotFound($"Location with id {pointId} not found");

            var response = Mapper.Map<GetByIdLocationsResponse>(location);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
    
    [HttpPost("")]
    [ProducesResponseType(typeof(CreateLocationsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Create([FromBody] CreateLocationsRequest request)
    {
        try
        {
            var location = Mapper.Map<Location>(request);
            
            await _locationService.CreateAsync(location);
            var response = Mapper.Map<CreateLocationsResponse>(location);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}