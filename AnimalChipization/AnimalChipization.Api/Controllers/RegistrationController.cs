using AnimalChipization.Api.Contracts.Account.Create;
using Microsoft.AspNetCore.Mvc;

namespace AnimalChipization.Api.Controllers;

[Route("registration")]
public class RegistrationController : ApiControllerBase
{
    public RegistrationController(ILogger<RegistrationController> logger) : base(logger)
    {
    }

    [HttpPost]
    public IActionResult Post(PostRegistrationRequest request)
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            return ExceptionResult(e);
        }
    }
}