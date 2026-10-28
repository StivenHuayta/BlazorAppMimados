using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Mimados_WebApp.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private static ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_currentUser));
    }

    public void Login(string email, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, email),
            new(ClaimTypes.Email, email)
        };

        if (role.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ||
            role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
        }
        else if (role.Equals("Veterinario", StringComparison.OrdinalIgnoreCase))
        {
            claims.Add(new Claim(ClaimTypes.Role, "Veterinario"));
            claims.Add(new Claim(ClaimTypes.Role, "Empleado"));
        }
        else if (role.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
        {
            claims.Add(new Claim(ClaimTypes.Role, "Cliente"));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var identity = new ClaimsIdentity(claims, "MimadosAuthScheme", ClaimTypes.Name, ClaimTypes.Role);
        _currentUser = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    public void Logout()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    public bool IsAuthenticated => _currentUser.Identity?.IsAuthenticated ?? false;
    public string CurrentUserEmail => _currentUser.Identity?.Name ?? string.Empty;
    public bool IsInRole(string role) => _currentUser.IsInRole(role);
}

