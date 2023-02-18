using AnimalChipization.Api.Contracts.Animals.Create;
using AnimalChipization.Api.Contracts.AnimalsTypes.Create;
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

    public AnimalsController(ILogger<ApiControllerBase> logger, IMapper mapper, IAnimalService animalService) : base(logger, mapper)
    {
        _animalService = animalService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateAnimalsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("AllowAnonymous")]
    // [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Create([FromBody] CreateAnimalsRequest request)
    {
        try
        {
            var model = Mapper.Map<CreateAnimalModel>(request);
            var animal = await _animalService.CreateAsync(model);
            var response = Mapper.Map<CreateAnimalsResponse>(animal);
            return Created($"/animals/types/{response.Id}", response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}