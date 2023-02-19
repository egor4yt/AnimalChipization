using AnimalChipization.Api.Contracts.AnimalsVisitedLocations;
using AnimalChipization.Core.Validation;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("animals/{animalId:long}/locations")]
public class AnimalsVisitedLocationsController : ApiControllerBase
{
    private readonly IAnimalVisitedLocationService _animalVisitedLocationService;

    public AnimalsVisitedLocationsController(ILogger<ApiControllerBase> logger, IMapper mapper, IAnimalVisitedLocationService animalVisitedLocationService) : base(logger, mapper)
    {
        _animalVisitedLocationService = animalVisitedLocationService;
    }

    [HttpPost("{pointId:long}")]
    [ProducesResponseType(typeof(AddAnimalsVisitedLocationsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Add([FromRoute] [GreaterThan(0L)] long animalId, [GreaterThan(0L)] [FromRoute] long pointId)
    {
        try
        {
            var visitedLocation = await _animalVisitedLocationService.AddAsync(animalId, pointId);
            var response = Mapper.Map<AddAnimalsVisitedLocationsResponse>(visitedLocation);
            return Created($"/animals/{response.Id}", response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}