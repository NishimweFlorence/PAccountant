using System.Security.Claims;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Infrastructure.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IUserService _userService;
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(IUserService userService)
        {
            _userService = userService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            if (userSession != null)
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Email),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("FirstName", userSession.FirstName),
                    new Claim("LastName", userSession.LastName),
                    new Claim("ProfilePictureUrl", userSession.ProfilePictureUrl ?? string.Empty)
                }, "CustomAuth"));
            }
            else
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            await Task.CompletedTask;
        }
    }

    public class UserSession
    {   
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
