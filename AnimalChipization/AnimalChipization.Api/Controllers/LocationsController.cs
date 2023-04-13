using AnimalChipization.Api.Contracts.Locations.Create;
using AnimalChipization.Api.Contracts.Locations.GetById;
using AnimalChipization.Api.Contracts.Locations.Update;
using AnimalChipization.Api.Contracts.Shared;
using AnimalChipization.Api.Contracts.Validation;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
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

    public LocationsController(ILogger<LocationsController> logger, IMapper mapper, ILocationService locationService) :
        base(logger, mapper)
    {
        _locationService = locationService;
    }

    [HttpGet]
    [Produces("text/plain")]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetLocationIdByCoordinates([FromQuery] CoordinatesRequestItem request)
    {
        try
        {
            var location = await _locationService.SearchByCoordinates(request.Latitude, request.Longitude);
            if (location is null) return NotFound($"Location with latitude {request.Latitude} and longitude {request.Longitude} does not exists");


            return Ok(location.Id.ToString());
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpGet("{pointId:long}")]
    [ProducesResponseType(typeof(GetByIdLocationsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> GetById([FromRoute] [GreaterThan(0L)] long pointId)
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

    [HttpPost]
    [ProducesResponseType(typeof(CreateLocationsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
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

    [HttpPut("{pointId:long}")]
    [ProducesResponseType(typeof(UpdateLocationsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
    public async Task<IActionResult> Update([FromRoute] [GreaterThan(0L)] long pointId, [FromBody] UpdateLocationsRequest request)
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

    [HttpDelete("{pointId:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Delete([FromRoute] [GreaterThan(0L)] long pointId)
    {
        try
        {
            await _locationService.DeleteAsync(pointId);
            return Ok();
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}