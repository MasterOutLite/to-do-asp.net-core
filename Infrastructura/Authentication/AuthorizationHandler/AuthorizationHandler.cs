using Infrastructure.Authentication.AuthorizationRequirement;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace Infrastructure.Authentication.AuthorizationHandler;

public class AuthorizationHandler
    : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement
    )
    {
        string? userId = context.User.Claims.FirstOrDefault(
            x => x.Type == JwtClaims.Id)?.Value;

        if (userId is null)
        {
            return Task.FromCanceled(default);
            //return Task.FromException(new ArgumentException("Fail read token"));
        }

        var role = context.User.Claims
            .Where(c => c.Type == JwtClaims.Role)
            .Select(v => v.Value)
            .ToHashSet();

        Log.Information("User id: {@userId}. Attribute role: {@role}. User role: {@userRole}",
            userId, requirement.Role, role);

        if (role.Contains(requirement.Role))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}