using Domain.Entities;

namespace Domain.Abstractions.Repository;

public interface IToDoRepositoryQuery
{
    Task<ToDo?> GetById (long id);

    Task<ToDo?> GetByIdAndUserId (long id, long userId);

    Task<IEnumerable<ToDo>> GetAllByIdAndUserId (long userId, long? categoryId);
}