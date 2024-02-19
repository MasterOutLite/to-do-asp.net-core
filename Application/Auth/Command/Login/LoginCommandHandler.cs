using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Extensions;
using Application.Common.Models;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Auth.Command.Login;

public class LoginCommandHandler(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IApplicationDbContext dbContext
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

        var token = user.CreateClaims(new())
            .EncodedToken(configuration)
            .GetToken();

        return new ResponseToken(token);
    }
}