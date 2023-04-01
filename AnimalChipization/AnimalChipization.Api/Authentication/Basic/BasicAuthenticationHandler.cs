using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using AnimalChipization.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace AnimalChipization.Api.Authentication.Basic;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IAccountService _accountService;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IAccountService accountService) : base(options, logger, encoder, clock)
    {
        _accountService = accountService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Request.Headers.TryGetValue("Authorization", out var authorizationHeader) == false) return AuthenticateResult.Fail("Authorization header is missing");
        Response.Headers.Add("WWW-Authenticate", "Basic");
        
        var authHeaderRegex = new Regex(@"Basic (.*)");
        if (authHeaderRegex.IsMatch(authorizationHeader.ToString()) == false) return AuthenticateResult.Fail("Authorization code not formatted properly");
        
        var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.Replace(authorizationHeader.ToString(), "$1")));
        var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
        var authEmail = authSplit[0];
        var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get password");

        var account = await _accountService.AuthenticateAsync(authEmail, authPassword);
        if (account == null) return AuthenticateResult.Fail("The username or password is not correct.");
        
        var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, account.FirstName);
        var claims = new List<Claim>
        {
            new("FirstName", account.FirstName),
            new("LastName", account.LastName),
            new("Email", account.Email),
            new("UserId", account.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(authenticatedUser, claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
    }
}