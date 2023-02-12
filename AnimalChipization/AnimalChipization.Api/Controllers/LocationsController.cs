using AnimalChipization.Api.Contracts.Locations.Create;
using AnimalChipization.Api.Contracts.Locations.GetById;
using AnimalChipization.Api.Contracts.Locations.Update;
using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.Location;
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
            return Created($"/locations/{location.Id}", response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpPut("{pointId:int}")]
    [ProducesResponseType(typeof(UpdateLocationsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Update([FromRoute] int pointId, [FromBody] UpdateLocationsRequest request)
    {
        try
        {
            var updateModel = Mapper.Map<UpdateLocationModel>(request);
            updateModel.Id = pointId;

            var location = await _locationService.UpdateAsync(updateModel);
            var response = Mapper.Map<UpdateLocationsResponse>(location);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}