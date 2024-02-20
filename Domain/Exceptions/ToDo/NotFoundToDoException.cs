using Domain.Exceptions.Base;

namespace Domain.Exceptions.ToDo;

public sealed class NotFoundToDoException(long id) :
    NotFoundException($"Not found ToDo by id {id}!");