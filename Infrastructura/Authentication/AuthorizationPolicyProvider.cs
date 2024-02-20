﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication;

internal class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        return new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .AddRequirements(new RoleRequirement(policyName))
            .RequireAuthenticatedUser()
            .Build();
    }
}