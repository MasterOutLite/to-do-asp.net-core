using System.Security.Claims;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Authentication;

public class AuthorizationHandler(IServiceScopeFactory scopeFactory)
    : AuthorizationHandler<RoleRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement
    )
    {
        string? userId = context.User.Claims.FirstOrDefault(
            x => x.Type == JwtClaims.Id)?.Value;

        Log.Information("User id: {@userId}.",
            userId);

        if (userId is null)
        {
            return;
        }

        IServiceScope scope = scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByIdAsync(userId);
        var role = await userManager.GetRolesAsync(user!);

        Log.Information("Attribute role: {@role}. User role: {@userRole}",
            requirement.Role, role);

        if (role.Contains(requirement.Role))
        {
            context.Succeed(requirement);
        }
    }
}