namespace Domain.Exceptions.Base;

public abstract class ForbiddenException(string message) : Exception(message);