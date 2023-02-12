using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace AnimalChipization.Api.Authentication.Basic;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IAccountRepository _accountRepository;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock, IAccountRepository accountRepository)
        : base(options, logger, encoder, clock)
    {
        _accountRepository = accountRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Response.Headers.Add("WWW-Authenticate", "Basic");

        if (Request.Headers.TryGetValue("Authorization", out var authorizationHeader) == false)
            return AuthenticateResult.Fail("Authorization header missing.");

        var authHeaderRegex = new Regex(@"Basic (.*)");
        if (authHeaderRegex.IsMatch(authorizationHeader) == false)
            return AuthenticateResult.Fail("Authorization code not formatted properly.");

        var authBase64 =
            Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.Replace(authorizationHeader, "$1")));
        var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
        var authEmail = authSplit[0];
        var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get password");

        var account = await _accountRepository.AuthenticateUserAsync(authEmail, authPassword);
        if (account == null) return AuthenticateResult.Fail("The username or password is not correct.");

        var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, account.FirstName);
        var claims = new List<Claim>
        {
            new("FirstName", account.FirstName),
            new("LastName", account.LastName),
            new("Email", account.Email),
            new("UserId", account.Id.ToString())
        };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser, claims));

        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
    }
}