using AnimalChipization.Api.Contracts.AnimalsTypes.Post;
using AnimalChipization.Api.Contracts.Registration.Post;
using AnimalChipization.Data.Entities;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
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
    
    [HttpPost]
    [ProducesResponseType(typeof(PostAnimalsTypesResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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