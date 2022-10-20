
using ATM.Models;
using ATM.Pages.Account;

using Microsoft.AspNetCore.Components.Authorization;

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ATM.Security
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IUserSession _userSession;

        public CustomAuthenticationStateProvider(IUserSession userSession)
        {
            this._userSession = userSession;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var loggedInUser = await _userSession.GetLoggedInUser();

            if (loggedInUser is null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, loggedInUser.Email) }, "dummy")));
        }

        public async Task MarkUserAsAuthenticated(LoginViewModel login)
        {
            var result = await _userSession.Login(login);

            if (result)
            {
                var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, login.Email) }, "dummy"));
                var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
                NotifyAuthenticationStateChanged(authState);
            }
        }

        public Task MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
            return Task.CompletedTask;
        }

    }
}
