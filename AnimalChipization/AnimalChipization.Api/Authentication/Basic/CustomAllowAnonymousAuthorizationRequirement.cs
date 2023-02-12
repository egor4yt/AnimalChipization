using Microsoft.AspNetCore.Authorization;

namespace AnimalChipization.Api.Authentication.Basic;

public class CustomAllowAnonymousAuthorizationRequirement : IAuthorizationRequirement
{
    public CustomAllowAnonymousAuthorizationRequirement(bool allowAnonymous)
    {
        AllowAnonymous = allowAnonymous;
    }

    public bool AllowAnonymous { get; set; }
}