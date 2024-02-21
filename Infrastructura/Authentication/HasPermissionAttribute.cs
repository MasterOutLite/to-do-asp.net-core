using Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public sealed class HasPermissionAttribute(Role role)
    : AuthorizeAttribute(policy: role.ToString());