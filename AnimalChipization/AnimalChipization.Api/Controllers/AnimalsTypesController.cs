using AnimalChipization.Api.Contracts.AnimalsTypes.Create;
using AnimalChipization.Api.Contracts.AnimalsTypes.GetById;
using AnimalChipization.Api.Contracts.AnimalsTypes.Update;
using AnimalChipization.Core.Validation;
using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Models.AnimalType;
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

    [HttpGet("{animalTypeId:long}")]
    [ProducesResponseType(typeof(GetByIdAnimalsTypesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetById([FromRoute] [GreaterThan(0L)] long animalTypeId)
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
    [ProducesResponseType(typeof(CreateAnimalsTypesResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Create([FromBody] CreateAnimalsTypesRequest request)
    {
        try
        {
            var animalType = Mapper.Map<AnimalType>(request);
            await _animalTypeService.CreateAsync(animalType);

            var response = Mapper.Map<CreateAnimalsTypesResponse>(animalType);
            return Created($"/animals/types/{response.Id}", response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpPut("{animalTypeId:long}")]
    [ProducesResponseType(typeof(UpdateAnimalsTypesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Update([FromRoute] [GreaterThan(0L)] long animalTypeId, [FromBody] UpdateAnimalsTypesRequest request)
    {
        try
        {
            var updateModel = Mapper.Map<UpdateAnimalTypeModel>(request);
            updateModel.Id = animalTypeId;

            var animalType = await _animalTypeService.UpdateAsync(updateModel);
            var response = Mapper.Map<UpdateAnimalsTypesResponse>(animalType);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}