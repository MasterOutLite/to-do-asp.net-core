using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class NotFoundCategoryException(long id) :
    NotFoundException($"Not found category of user by id {id}!");