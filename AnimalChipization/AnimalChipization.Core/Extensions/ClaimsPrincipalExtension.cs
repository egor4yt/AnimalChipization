using System.Security.Claims;

namespace AnimalChipization.Core.Extensions;

public static class ClaimsPrincipalExtension
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.Any(x => x.Type == "UserId")
            ? int.Parse(claimsPrincipal.Claims.First(x => x.Type == "UserId").Value)
            : 0;
    }
}