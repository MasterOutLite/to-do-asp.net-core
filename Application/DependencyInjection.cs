using System.Reflection;
using Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var application = Assembly.GetExecutingAssembly();
            services.AddMediatR(cnf => { cnf.RegisterServicesFromAssembly(application); });

            services.AddValidatorsFromAssembly(application);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;
        }
    }
}