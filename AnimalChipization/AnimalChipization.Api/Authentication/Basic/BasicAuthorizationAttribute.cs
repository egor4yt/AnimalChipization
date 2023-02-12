using Microsoft.AspNetCore.Authorization;

namespace AnimalChipization.Api.Authentication.Basic;

public class BasicAuthorizationAttribute : AuthorizeAttribute
{
    public BasicAuthorizationAttribute()
    {
        Policy = "BasicAuthentication";
    }
}