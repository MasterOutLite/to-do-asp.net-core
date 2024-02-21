using Domain.Constants;
using Infrastructure.Authentication.AuthorizationRequirement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace api.OptionSetup;

public class AuthorizationOptionsSetup : IConfigureNamedOptions<AuthorizationOptions>
{
    public void Configure(AuthorizationOptions options)
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();

        options.AddPolicy("Base",
            new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());

        //options.AddPolicy(Role.User.ToString(), new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        //    .RequireAuthenticatedUser()
        //    .AddRequirements(new RoleRequirement(Role.User.ToString()))
        //    .Build());
    }

    public void Configure(string? name, AuthorizationOptions options)
    {
        Configure(options);
    }
}