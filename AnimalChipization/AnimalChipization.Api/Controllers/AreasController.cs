using System.Globalization;
using AnimalChipization.Api.Contracts.Accounts.Create;
using AnimalChipization.Api.Contracts.Areas.Create;
using AnimalChipization.Api.Contracts.Areas.GetById;
using AnimalChipization.Api.Contracts.Areas.Update;
using AnimalChipization.Api.Contracts.Shared;
using AnimalChipization.Api.Contracts.Validation;
using AnimalChipization.Core.Exceptions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Services.Models.Area;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("[controller]")]
public class AreasController : ApiControllerBase
{
    private readonly IAreaService _areaService;

    public AreasController(IMapper mapper,
        IAreaService areaService) :
        base(mapper)
    {
        _areaService = areaService;
    }

    [HttpGet("{areaId:long}")]
    [ProducesResponseType(typeof(GetByIdAreasResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> GetById([FromRoute] [GreaterThan(0L)] long areaId)
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

    [HttpPost]
    [ProducesResponseType(typeof(CreateAccountsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [Authorize("RequireAuthenticated", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Create([FromBody] CreateAreasRequest request)
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

    [HttpDelete("{areaId:long}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Delete([FromRoute] [GreaterThan(0L)] long areaId)
    {
        await _areaService.DeleteAsync(areaId);
        return Ok();
    }

    [HttpPut("{areaId:long}")]
    [ProducesResponseType(typeof(UpdateAreasResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Update([FromRoute] [GreaterThan(0L)] long areaId, [FromBody] UpdateAreasRequest request)
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
}