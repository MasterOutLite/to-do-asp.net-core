using Application.Abstractions.Interfaces;
using Domain.Entities;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            builder =>
                builder.UseNpgsql(
                    configuration.GetConnectionString("PostgresConnect")
                )
        );

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddDefaultTokenProviders();

        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddSingleton<IAuthorizationHandler, AuthorizationOwnHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

        return services;
    }
}