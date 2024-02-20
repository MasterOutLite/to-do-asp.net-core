using Domain.Exceptions.Base;

namespace Domain.Exceptions.User;

public class UnauthorizedFailLoginException() : UnauthorizedException(
    "Failed to authorize user. Invalid login or password!");