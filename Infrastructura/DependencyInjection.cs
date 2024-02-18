using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            builder =>
                builder.UseNpgsql(
                    "Host=localhost;" +
                    "Port=5432;" +
                    "Database=todo;" +
                    "Username=postgres;" +
                    "Password=root;")
        );
        return services;
    }
}