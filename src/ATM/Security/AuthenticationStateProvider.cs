
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

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, loggedInUser.AccountName) }, "jwt")));
        }

        public void MarkUserAsAuthenticated(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
