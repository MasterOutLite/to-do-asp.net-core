using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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

        Console.WriteLine(configuration.GetConnectionString("PostgresConnect")!);

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            // services.AddIdentityCore<ApplicationUser>()
            //     .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        return services;
    }
}