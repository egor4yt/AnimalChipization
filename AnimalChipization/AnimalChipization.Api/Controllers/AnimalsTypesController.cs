using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.AnimalsTypes.GetById;
using AnimalChipization.Api.Contracts.AnimalsTypes.Post;
using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("animals/types")]
public class AnimalsTypesController : ApiControllerBase
{
    private readonly IAnimalTypeService _animalTypeService;

    public AnimalsTypesController(ILogger<ApiControllerBase> logger, IMapper mapper, IAnimalTypeService animalTypeService) : base(logger, mapper)
    {
        _animalTypeService = animalTypeService;
    }

    [HttpGet("{animalTypeId:int}")]
    [ProducesResponseType(typeof(GetByIdAnimalsTypesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetById([FromRoute] [Range(1, long.MaxValue)] long animalTypeId)
    {
        try
        {
            var animalType = await _animalTypeService.GetByIdAsync(animalTypeId);
            if (animalType is null) return NotFound($"Animal type with id {animalTypeId} not found");

            var response = Mapper.Map<GetByIdAnimalsTypesResponse>(animalType);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(PostAnimalsTypesResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string),StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Post([FromBody] PostAnimalsTypesRequest request)
    {
        try
        {
            var animalType = Mapper.Map<AnimalType>(request);
            await _animalTypeService.CreateAsync(animalType);

            var response = Mapper.Map<PostAnimalsTypesResponse>(animalType);
            return Created($"/animals/types/{response.Id}", response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}