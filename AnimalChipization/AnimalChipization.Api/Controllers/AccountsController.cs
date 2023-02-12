using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace AnimalChipization.Api.Controllers;

[Authorize]
public class AccountsController : ApiControllerBase
{
    public AccountsController(ILogger<AccountsController> logger, IMapper mapper) : base(logger,mapper)
    {
    }
}