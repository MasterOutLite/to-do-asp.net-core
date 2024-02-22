using Application.Abstractions.Interfaces;
using Domain.Abstractions.Repository;
using Domain.Entities;
using Infrastructure.Authentication;
using Infrastructure.Authentication.AuthorizationHandler;
using Infrastructure.Authentication.Policy;
using Infrastructure.Data;
using Infrastructure.Options;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddTransient<IAuthorizationHandler, AuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IToDoRepository, ToDoRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();

        return services;
    }
}