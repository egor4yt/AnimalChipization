using Microsoft.AspNetCore.Authorization;

namespace AnimalChipization.Api.Authentication.Basic;

public class
    CustomAllowAnonymousAuthorizationHandler : AuthorizationHandler<CustomAllowAnonymousAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CustomAllowAnonymousAuthorizationRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
        {
            context.Fail();
            return Task.CompletedTask;
        }

//
        switch (context.User.Identity?.IsAuthenticated)
        {
            case true:
                context.Succeed(requirement);
                break;
            case false:
                if (httpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Fail();
                }
                else
                {
                    if (requirement.AllowAnonymous) context.Succeed(requirement);
                    else context.Fail();
                }

                break;
        }

        return Task.CompletedTask;
    }
}