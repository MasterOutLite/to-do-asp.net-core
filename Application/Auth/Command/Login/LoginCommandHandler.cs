using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Domain.Exceptions.User;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Application.Auth.Command.Login;

public class LoginCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider
)
    : ICommandHandler<LoginCommand, ResponseToken>
{
    public async Task<ResponseToken> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new UnauthorizedFailLoginException();
        }

        bool isValidPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isValidPassword)
        {
            throw new UnauthorizedFailLoginException();
        }

        var role = await userManager.GetRolesAsync(user);

        Log.Information("User: {@user}. Role: {@role}", user, role);

        string token = jwtProvider.CreateToken(user, role);

        return new ResponseToken(token);
    }
}