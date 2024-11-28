using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SSOApplicationBlazor.Services
{
    public class AuthService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AuthenticationStateProvider authenticationStateProvider, IHttpContextAccessor httpContextAccessor)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await _authenticationStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<ClaimsPrincipal> GetCurrentUserAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            return authState.User;
        }

        public async Task<string> GetUserNameAsync()
        {
            var user = await GetCurrentUserAsync();
            return user.Identity?.Name ?? "Unknown";
        }

        public async Task<bool> IsUserAuthenticatedAsync()
        {
            var user = await GetCurrentUserAsync();
            return user.Identity?.IsAuthenticated ?? false;
        }

        public async Task<IEnumerable<Claim>> GetUserClaimsAsync()
        {
            var user = await GetCurrentUserAsync();
            return user.Claims;
        }
    }
}
