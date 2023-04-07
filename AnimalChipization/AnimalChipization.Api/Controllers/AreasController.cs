using System.Globalization;
using AnimalChipization.Api.Contracts.Accounts.Create;
using AnimalChipization.Api.Contracts.Areas.Create;
using AnimalChipization.Api.Contracts.Areas.GetById;
using AnimalChipization.Api.Contracts.Areas.Update;
using AnimalChipization.Api.Contracts.Shared;
using AnimalChipization.Api.Contracts.Validation;
using AnimalChipization.Core.Exceptions;
using AnimalChipization.Core.Helpers;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Services.Models.Area;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace AnimalChipization.Api.Controllers;

[Route("[controller]")]
public class AreasController : ApiControllerBase
{
    private readonly IAreaService _areaService;

    public AreasController(IMapper mapper,
        IAreaService areaService,
        ILogger<AreasController> logger) :
        base(logger, mapper)
    {
        _areaService = areaService;
    }

    [HttpGet("{areaId:long}")]
    [ProducesResponseType(typeof(GetByIdAreasResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetById([FromRoute] [GreaterThan(0L)] long areaId)
    {
        try
        {
            var area = await _areaService.GetByIdAsync(areaId);
            if (area is null) throw new NotFoundException($"Area with id {areaId} not found");

            var response = Mapper.Map<GetByIdAreasResponse>(area);
            response.AreaPoints = area.AreaPoints.Split(";").Select(
                x => new CoordinatesRequestItem
                {
                    Latitude = double.Parse(x.Split(",")[0], CultureInfo.InvariantCulture),
                    Longitude = double.Parse(x.Split(",")[1], CultureInfo.InvariantCulture)
                }).ToList();
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateAccountsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [Authorize("AllowAnonymous", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Create([FromBody] CreateAreasRequest request)
    {
        try
        {
            var area = Mapper.Map<Area>(request);
            await _areaService.CreateAsync(area);

            var response = Mapper.Map<CreateAreasResponse>(area);
            response.AreaPoints = area.AreaPoints.Split(";").Select(
                x => new CoordinatesRequestItem
                {
                    Latitude = double.Parse(x.Split(",")[0], CultureInfo.InvariantCulture),
                    Longitude = double.Parse(x.Split(",")[1], CultureInfo.InvariantCulture)
                }).ToList();
            
            return Created($"/areas/{response.Id}", response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpDelete("{areaId:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("AllowAnonymous", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Delete([FromRoute] [GreaterThan(0L)] long areaId)
    {
        try
        {
            await _areaService.DeleteAsync(areaId);
            return Ok();
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpPut("{areaId:long}")]
    [ProducesResponseType(typeof(UpdateAreasResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("AllowAnonymous", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Update([FromRoute] [GreaterThan(0L)] long areaId, [FromBody] UpdateAreasRequest request)
    {
        try
        {
            var updateAreaModel = Mapper.Map<UpdateAreaModel>(request);
            updateAreaModel.Id = areaId;
            
            var updatedArea = await _areaService.UpdateAsync(updateAreaModel);
            var response = Mapper.Map<UpdateAreasResponse>(updatedArea);
            
            response.AreaPoints = updatedArea.AreaPoints.Split(";").Select(
                x => new CoordinatesRequestItem
                {
                    Latitude = double.Parse(x.Split(",")[0], CultureInfo.InvariantCulture),
                    Longitude = double.Parse(x.Split(",")[1], CultureInfo.InvariantCulture)
                }).ToList();
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}