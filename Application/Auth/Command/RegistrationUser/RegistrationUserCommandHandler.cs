using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions.User;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Application.Auth.Command.RegistrationUser;

public class RegistrationUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider
)
    : ICommandHandler<RegistrationUserCommand, ResponseToken>
{
    public async Task<ResponseToken> Handle(
        RegistrationUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var exist = await userManager.FindByNameAsync(request.UserName);
        if (exist is not null)
        {
            throw new BadRequestUserExistException();
        }

        var user = request.Adapt<ApplicationUser>();
        var resultUser = await userManager.CreateAsync(user, request.Password);

        if (!resultUser.Succeeded)
        {
            Log.Information("Succeeded: {suc}. Error: {@error}", resultUser.Succeeded, resultUser.Errors);
            throw new UnauthorizedFailLoginException();
        }

        var resultRole = await userManager.AddToRoleAsync(user, Role.User.ToString());
        if (!resultRole.Succeeded)
        {
            Log.Information("User: {@user}. Error: {@error}", user, resultRole.Errors);
        }

        var role = await userManager.GetRolesAsync(user);
        Log.Information("User: {@user}. Role: {@role}", user, role);

        var token = jwtProvider.CreateToken(user, role);

        return new ResponseToken(token);
    }
}