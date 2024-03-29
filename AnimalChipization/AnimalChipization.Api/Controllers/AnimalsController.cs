using AnimalChipization.Api.Contracts.Animals.AttachAnimalType;
using AnimalChipization.Api.Contracts.Animals.ChangeAnimalTypeAnimals;
using AnimalChipization.Api.Contracts.Animals.Create;
using AnimalChipization.Api.Contracts.Animals.DeleteAnimalType;
using AnimalChipization.Api.Contracts.Animals.GetById;
using AnimalChipization.Api.Contracts.Animals.Search;
using AnimalChipization.Api.Contracts.Animals.Update;
using AnimalChipization.Api.Contracts.Validation;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Services.Models.Animal;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("animals")]
public class AnimalsController : ApiControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalsController(IMapper mapper,
        IAnimalService animalService) : base(mapper)
    {
        _animalService = animalService;
    }

    [HttpGet("{animalId:long}")]
    [ProducesResponseType(typeof(GetByIdAnimalsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> GetById([FromRoute] [GreaterThan(0L)] long animalId)
    {
        var animal = await _animalService.GetByIdAsync(animalId);
        if (animal is null) return NotFound($"Animal with id {animalId} not found");

        var response = Mapper.Map<GetByIdAnimalsResponse>(animal);
        return Ok(response);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<SearchAnimalsResponseItem>), StatusCodes.Status200OK)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Search([FromQuery] SearchAnimalsRequests request)
    {
        var searchModel = Mapper.Map<SearchAnimalModel>(request);
        var animals = await _animalService.SearchAsync(searchModel);
        var response = Mapper.Map<IEnumerable<SearchAnimalsResponseItem>>(animals);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateAnimalsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
    public async Task<IActionResult> Create([FromBody] CreateAnimalsRequest request)
    {
        var model = Mapper.Map<CreateAnimalModel>(request);
        var animal = await _animalService.CreateAsync(model);
        var response = Mapper.Map<CreateAnimalsResponse>(animal);
        return Created($"/animals/types/{response.Id}", response);
    }

    [HttpPut("{animalId:long}")]
    [ProducesResponseType(typeof(UpdateAnimalsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
    public async Task<IActionResult> Update([FromRoute] [GreaterThan(0L)] long animalId, [FromBody] UpdateAnimalsRequest request)
    {
        var updateModel = Mapper.Map<UpdateAnimalModel>(request);
        updateModel.Id = animalId;

        var animal = await _animalService.UpdateAsync(updateModel);
        var response = Mapper.Map<UpdateAnimalsResponse>(animal);
        return Ok(response);
    }

    [HttpPost("{animalId:long}/types/{animalTypeId:long}")]
    [ProducesResponseType(typeof(AttachAnimalTypeAnimalsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
    public async Task<IActionResult> AttachAnimalType([FromRoute] [GreaterThan(0L)] long animalId, [FromRoute] [GreaterThan(0L)] long animalTypeId)
    {
        var animal = await _animalService.AttachAnimalTypeAsync(animalId, animalTypeId);
        var response = Mapper.Map<AttachAnimalTypeAnimalsResponse>(animal);
        return Created($"/animals/{response.Id}", response);
    }

    [HttpPut("{animalId:long}/types")]
    [ProducesResponseType(typeof(ChangeAnimalTypeAnimalsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
    public async Task<IActionResult> ChangeAnimalType([FromRoute] [GreaterThan(0L)] long animalId, [FromBody] ChangeAnimalTypeAnimalsRequest request)
    {
        var model = Mapper.Map<ChangeAnimalTypeAnimalModel>(request);
        model.AnimalId = animalId;

        var animal = await _animalService.ChangeAnimalTypeAsync(model);
        var response = Mapper.Map<ChangeAnimalTypeAnimalsResponse>(animal);
        return Ok(response);
    }

    [HttpDelete("{animalId:long}/types/{animalTypeId:long}")]
    [ProducesResponseType(typeof(DeleteAnimalTypeAnimalsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = $"{AccountRole.Administrator},{AccountRole.Chipper}")]
    public async Task<IActionResult> DetachAnimalType([FromRoute] [GreaterThan(0L)] long animalId, [GreaterThan(0L)] [FromRoute] long animalTypeId)
    {
        var animal = await _animalService.DetachAnimalTypeAsync(animalId, animalTypeId);
        var response = Mapper.Map<DeleteAnimalTypeAnimalsResponse>(animal);
        return Ok(response);
    }

    [HttpDelete("{animalId:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Delete([FromRoute] [GreaterThan(0L)] long animalId)
    {
        await _animalService.DeleteAsync(animalId);
        return Ok();
    }
}