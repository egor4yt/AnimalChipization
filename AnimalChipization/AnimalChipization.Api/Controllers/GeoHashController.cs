using AnimalChipization.Api.Contracts.Shared;
using AnimalChipization.Core.Helpers;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Geohash;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("locations")]
public class GeoHashController : ApiControllerBase
{
    private readonly ILocationService _locationService;

    public GeoHashController(IMapper mapper,
        ILocationService locationService) : base(mapper)
    {
        _locationService = locationService;
    }


    [HttpGet("geohash")]
    [Produces("text/plain")]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetGeoHash([FromQuery] CoordinatesRequestItem request)
    {
        var location = await _locationService.SearchByCoordinates(request.Latitude, request.Longitude);
        if (location is null) return NotFound($"Location with latitude {request.Latitude} and longitude {request.Longitude} does not exists");

        var hasher = new Geohasher();
        var geoHash = hasher.Encode(location.Latitude, location.Longitude, 12);
        return Ok(geoHash);
    }

    [HttpGet("geohashv2")]
    [Produces("text/plain")]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetGeoHashV2([FromQuery] CoordinatesRequestItem request)
    {
        var location = await _locationService.SearchByCoordinates(request.Latitude, request.Longitude);
        if (location is null) return NotFound($"Location with latitude {request.Latitude} and longitude {request.Longitude} does not exists");

        var hasher = new Geohasher();
        var geoHash = hasher.Encode(location.Latitude, location.Longitude, 12);
        var geoHashAsBase64 = SecurityHelper.Base64Encode(geoHash);
        return Ok(geoHashAsBase64);
    }

    [HttpGet("geohashv3")]
    [Produces("text/plain")]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetGeoHashV3([FromQuery] CoordinatesRequestItem request)
    {
        var location = await _locationService.SearchByCoordinates(request.Latitude, request.Longitude);
        if (location is null) return NotFound($"Location with latitude {request.Latitude} and longitude {request.Longitude} does not exists");

        var hasher = new Geohasher();
        var geoHash = hasher.Encode(location.Latitude, location.Longitude, 12);
        var reversedGeoHashMd5Checksum = SecurityHelper.ComputeMd5CheckSum(geoHash).Reverse().ToArray();
        var base64StringResponse = Convert.ToBase64String(reversedGeoHashMd5Checksum);
        return Ok(base64StringResponse);
    }
}