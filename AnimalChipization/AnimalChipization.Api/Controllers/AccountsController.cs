using AnimalChipization.Api.Contracts.Accounts.Create;
using AnimalChipization.Api.Contracts.Accounts.GetById;
using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Api.Contracts.Accounts.Update;
using AnimalChipization.Api.Contracts.Validation;
using AnimalChipization.Core.Exceptions;
using AnimalChipization.Core.Extensions;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
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

    public AccountsController(IMapper mapper,
        IAccountService accountService) :
        base(mapper)
    {
        _accountService = accountService;
    }

    [HttpGet("{accountId:int}")]
    [ProducesResponseType(typeof(GetByIdAccountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> GetById([FromRoute] [GreaterThan(0)] int accountId)
    {
        if (accountId != HttpContext.User.GetUserId() && HttpContext.User.IsInRole(AccountRole.Administrator) == false) throw new ForbiddenException($"Account with id {accountId} not found");

        var account = await _accountService.GetByIdAsync(accountId);
        if (account is null) throw new NotFoundException($"Account with id {accountId} not found");

        var response = Mapper.Map<GetByIdAccountsResponse>(account);
        return Ok(response);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<SearchAccountsResponseItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [Authorize("RequireAuthenticated", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Search([FromQuery] SearchAccountsRequests request)
    {
        var searchModel = Mapper.Map<SearchAccountModel>(request);
        var accounts = await _accountService.SearchAsync(searchModel);
        var response = Mapper.Map<IEnumerable<SearchAccountsResponseItem>>(accounts);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateAccountsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [Authorize("RequireAuthenticated", Roles = AccountRole.Administrator)]
    public async Task<IActionResult> Create([FromBody] CreateAccountsRequest request)
    {
        var account = Mapper.Map<Account>(request);
        await _accountService.RegisterAsync(account);

        var response = Mapper.Map<CreateAccountsResponse>(account);
        return Created($"/accounts/{response.Id}", response);
    }

    [HttpPut("{accountId:int}")]
    [ProducesResponseType(typeof(UpdateAccountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Update([FromRoute] [GreaterThan(0)] int accountId, [FromBody] UpdateAccountsRequest request)
    {
        if (accountId != HttpContext.User.GetUserId() && HttpContext.User.IsInRole(AccountRole.Administrator) == false) throw new ForbiddenException("You can update only your account");
        var updateModel = Mapper.Map<UpdateAccountModel>(request);
        updateModel.Id = accountId;

        var account = await _accountService.UpdateAsync(updateModel);
        var response = Mapper.Map<UpdateAccountsResponse>(account);
        return Ok(response);
    }

    [HttpDelete("{accountId:int}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    [Authorize("RequireAuthenticated")]
    public async Task<IActionResult> Delete([FromRoute] [GreaterThan(0)] int accountId)
    {
        if (HttpContext.User.GetUserId() != accountId && HttpContext.User.IsInRole(AccountRole.Administrator) == false) throw new ForbiddenException("You can delete only your account");

        await _accountService.DeleteAsync(accountId);
        return Ok();
    }
}