using AutoMapper;

namespace AnimalChipization.Api.Controllers;

public class AccountsController : ApiControllerBase
{
    public AccountsController(ILogger<AccountsController> logger, IMapper mapper) : base(logger,mapper)
    {
    }
}