using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public class UnauthorizedFailLoginException() : UnauthorizedException(
    "Failed to authorize user. Invalid login or password!");