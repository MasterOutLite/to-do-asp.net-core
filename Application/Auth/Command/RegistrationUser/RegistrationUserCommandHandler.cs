using Application.Abstractions.Messaging;
using Application.Common.Extensions;
using Application.Common.Models;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Application.Auth.Command.RegistrationUser;

public class RegistrationUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IConfiguration configuration
)
    : ICommandHandler<RegistrationUserCommand, ResponseToken>
{
    public async Task<ResponseToken> Handle(
        RegistrationUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = request.Adapt<ApplicationUser>();

        var res = await userManager.CreateAsync(user, request.Password);

        if (!res.Succeeded)
        {
            Log.Information("Succeeded: {suc}. Error: {@error}", res.Succeeded, res.Errors);
            throw new UnauthorizedFailLoginException();
        }

        //await userManager.AddToRoleAsync(user, Role.Member);

        string token = user.CreateClaims(new())
            .EncodedToken(configuration)
            .GetToken();

        return new ResponseToken(token);
    }
}