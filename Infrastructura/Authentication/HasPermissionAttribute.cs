using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public sealed class HasPermissionAttribute(Permissions permission)
    : AuthorizeAttribute(policy: permission.ToString());