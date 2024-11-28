using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SSOApplicationBlazor.Providers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User.Identity?.IsAuthenticated ?? false)
            {
                var authState = new AuthenticationState(httpContext.User);
                return Task.FromResult(authState);
            }

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        }
    }
}
