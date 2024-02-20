using Domain.Exceptions.Base;

namespace Domain.Exceptions.User;

public class NotFoundUserException(long id) : NotFoundException($"Not found user by id {id}!");