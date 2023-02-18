using AnimalChipization.Api.Contracts.Accounts.GetById;
using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Accounts.Update;
using AnimalChipization.Core.Extensions;
using AnimalChipization.Services.Models.Account;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("[controller]")]
public class AccountsController : ApiControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(ILogger<AccountsController> logger, IMapper mapper, IAccountService accountService) :
        base(logger, mapper)
    {
        _accountService = accountService;
    }

    [HttpGet("{accountId:int}")]
    [ProducesResponseType(typeof(GetByIdAccountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> GetById([FromRoute] int accountId)
    {
        try
        {
            var account = await _accountService.GetByIdAsync(accountId);
            if (account is null) return NotFound($"Account with id {accountId} not found");

            var response = Mapper.Map<GetByIdAccountsResponse>(account);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<SearchAccountsResponse>), StatusCodes.Status200OK)]
    [Authorize("AllowAnonymous")]
    public async Task<IActionResult> Search([FromQuery] SearchAccountsRequests request)
    {
        try
        {
            var searchModel = Mapper.Map<SearchAccountModel>(request);
            var accounts = await _accountService.SearchAsync(searchModel);
            var response = Mapper.Map<IEnumerable<SearchAccountsResponse>>(accounts);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }

    [HttpPut("{accountId:int}")]
    [ProducesResponseType(typeof(IEnumerable<UpdateAccountsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Update([FromRoute] int accountId, [FromBody] UpdateAccountsRequest request)
    {
        try
        {
            if (int.Parse(HttpContext.User.GetUserId()) != accountId) return Forbid();

            var updateModel = Mapper.Map<UpdateAccountModel>(request);
            updateModel.Id = accountId;

            var account = await _accountService.UpdateAsync(updateModel);
            var response = Mapper.Map<UpdateAccountsResponse>(account);
            return Ok(response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}