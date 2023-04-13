using AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Add;
using AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Get;
using AnimalChipization.Api.Contracts.AnimalsVisitedLocations.Update;
using AnimalChipization.Api.Contracts.Validation;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Services.Models.AnimalVisitedLocation;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("animals/{animalId:long}/locations")]
public class AnimalsVisitedLocationsController : ApiControllerBase
{
    private readonly IAnimalVisitedLocationService _animalVisitedLocationService;

    public AnimalsVisitedLocationsController(IMapper mapper,
        IAnimalVisitedLocationService animalVisitedLocationService) : base(mapper)
    {
        _animalVisitedLocationService = animalVisitedLocationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAnimalsVisitedLocationsResponseItem>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Get([FromRoute] [GreaterThan(0L)] long animalId, [FromQuery] GetAnimalsVisitedLocationsRequest request)
    {
        var getModel = Mapper.Map<GetAnimalVisitedLocationModel>(request);
        getModel.AnimalId = animalId;
        var visitedLocations = await _animalVisitedLocationService.GetAsync(getModel);
        var response = Mapper.Map<IEnumerable<GetAnimalsVisitedLocationsResponseItem>>(visitedLocations);
        return Ok(response);
    }

    [HttpPost("{pointId:long}")]
    [ProducesResponseType(typeof(AddAnimalsVisitedLocationsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
    public async Task<IActionResult> Add([FromRoute] [GreaterThan(0L)] long animalId, [GreaterThan(0L)] [FromRoute] long pointId)
    {
        var visitedLocation = await _animalVisitedLocationService.AddAsync(animalId, pointId);
        var response = Mapper.Map<AddAnimalsVisitedLocationsResponse>(visitedLocation);
        return Created($"/animals/{response.Id}", response);
    }

    [HttpDelete("{visitedPointId:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> DeleteVisitedLocation([FromRoute] [GreaterThan(0L)] long animalId, [GreaterThan(0L)] [FromRoute] long visitedPointId)
    {
        await _animalVisitedLocationService.DeleteAsync(animalId, visitedPointId);
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType(typeof(IEnumerable<UpdateAnimalsVisitedLocationsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
    public async Task<IActionResult> Update([GreaterThan(0L)] long animalId, [FromBody] UpdateAnimalsVisitedLocationsRequest request)
    {
        var updateModel = Mapper.Map<UpdateAnimalVisitedLocationModel>(request);
        updateModel.AnimalId = animalId;

        var account = await _animalVisitedLocationService.UpdateAsync(updateModel);
        var response = Mapper.Map<UpdateAnimalsVisitedLocationsResponse>(account);
        return Ok(response);
    }
}