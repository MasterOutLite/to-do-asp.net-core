using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.AuthorizationRequirement;

public class RoleRequirement(string role) : IAuthorizationRequirement
{
    public string Role { get; } = role;
}