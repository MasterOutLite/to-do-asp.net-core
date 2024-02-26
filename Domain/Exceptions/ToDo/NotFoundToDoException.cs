using Domain.Exceptions.Base;

namespace Domain.Exceptions.ToDo;

public sealed class NotFoundToDoException(long id) :
    NotFoundException($"Not found todo of user by id {id}!");