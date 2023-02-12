using System.Security.Claims;

namespace AnimalChipization.Core.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.Any(x => x.Type == "UserId")
            ? claimsPrincipal.Claims.First(x => x.Type == "UserId").Value
            : "";
    }
}