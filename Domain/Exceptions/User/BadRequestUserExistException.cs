using Domain.Exceptions.Base;

namespace Domain.Exceptions.User;

public class BadRequestUserExistException() : BadRequestException("Exist user!");