using Application.Abstractions.Messaging;
using Domain.Constants;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Command.RegistrationUser;

public class RegistrationUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager
)
    : ICommandHandler<RegistrationUserCommand, string>
{
    public async Task<string> Handle(RegistrationUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.Adapt<ApplicationUser>();

        await userManager.CreateAsync(user, request.Password);
        await userManager.AddToRoleAsync(user, Role.Member);

        return "None";
    }
}