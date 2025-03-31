using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using EcoEarthPOC.Models;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = Setting.UserBasicDetail != null
            ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Setting.UserBasicDetail.Name) }, "CustomAuth")
            : new ClaimsIdentity();

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyAuthStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}