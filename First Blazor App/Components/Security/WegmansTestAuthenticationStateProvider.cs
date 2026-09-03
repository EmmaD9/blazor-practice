using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace First_Blazor_App.Components.Security;

public sealed class WegmansTestAuthenticationStateProvider : AuthenticationStateProvider
{
    private static readonly ClaimsPrincipal Anonymous =
        new(new ClaimsIdentity());

    private ClaimsPrincipal _currentUser = Anonymous;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
        => Task.FromResult(new AuthenticationState(_currentUser));

    public void SignInAsTeamMember()
    {
        _currentUser = CreateUser("wegmans-team-member", "TeamMember");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void SignInAsManager()
    {
        _currentUser = CreateUser("wegmans-manager", "Manager");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void SignOut()
    {
        _currentUser = Anonymous;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static ClaimsPrincipal CreateUser(string username, string role)
    {
        var identity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim("scope", "wegmans.security.test")
            ],
            authenticationType: "WegmansTestAuth");

        return new ClaimsPrincipal(identity);
    }
}
