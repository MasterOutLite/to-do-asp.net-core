using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication;

internal class AuthorizationHandler(IServiceScopeFactory scopeFactory) : AuthorizationHandler<RoleRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement
    )
    {
        string? userId = context.User.Claims.FirstOrDefault(
            x => x.Type == JwtClaims.Id)?.Value;


        if (!long.TryParse(userId, out long parseUserId))
        {
            return;
        }

        IServiceScope scope = scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByIdAsync(userId);
        await userManager.FindByIdAsync(userId);
        var role = await userManager.GetRolesAsync(user!);

        if (role.Contains(requirement.Role))
        {
            context.Succeed(requirement);
        }
    }
}