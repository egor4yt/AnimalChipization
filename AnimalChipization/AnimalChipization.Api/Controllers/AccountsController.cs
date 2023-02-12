using AnimalChipization.Api.Contracts.Accounts.GetById;
using AnimalChipization.Api.Contracts.Accounts.Search;
using AnimalChipization.Data.Repositories.Interfaces;
using AnimalChipization.Services.Models.Account;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class AccountsController : ApiControllerBase
{
    private readonly IAccountService _accountService;
    public AccountsController(ILogger<AccountsController> logger, IMapper mapper, IAccountService accountService) : base(logger,mapper)
    {
        _accountService = accountService;
    }

    [HttpGet("{accountId}")]
    [ProducesResponseType(typeof(GetByIdAccountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
}