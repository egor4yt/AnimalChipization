using AnimalChipization.Api.Contracts.Registration.Post;
using AnimalChipization.Data.Entities;
using AnimalChipization.Data.Entities.Constants;
using AnimalChipization.Services.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("registration")]
[AllowAnonymous]
public class RegistrationController : ApiControllerBase
{
    private readonly IAccountService _accountService;

    public RegistrationController(IMapper mapper,
        IAccountService accountService,
        ILogger<AccountsController> logger) : base(logger, mapper)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PostRegistrationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post([FromBody] PostRegistrationRequest request)
    {
        try
        {
            if (HttpContext.Request.Headers.Authorization.Count != 0) return Forbid();

            var account = Mapper.Map<Account>(request);
            account.Role = AccountRole.User;

            await _accountService.RegisterAsync(account);

            var response = Mapper.Map<PostRegistrationResponse>(account);
            return Created($"/accounts/{response.Id}", response);
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}