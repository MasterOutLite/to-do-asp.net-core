using Application.Abstractions.Interfaces;
using Domain.Abstractions.Repository;
using Domain.Entities;
using Infrastructure.Authentication;
using Infrastructure.Authentication.AuthorizationHandler;
using Infrastructure.Authentication.Policy;
using Infrastructure.Data;
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
        services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseNpgsql(
                configuration.GetConnectionString("PostgresConnect")
            )
        );

        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>()
        );

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddDefaultTokenProviders();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddTransient<IAuthorizationHandler, AuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IToDoRepository, ToDoRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IToDoRepositoryQuery, ToDoRepositoryQuery>();
        services.AddScoped<ICategoryRepositoryQuery, CategoryRepositoryQuery>();

        return services;
    }
}