using Domain.Entities;

namespace Domain.Abstractions.Repository;

public interface IToDoRepository
{
    void Add(ToDo toDo);
    void Remove(ToDo toDo);
    void Update(ToDo toDo);
    Task<ToDo?> GetById(long id);

    Task<ToDo?> GetByIdAndUserId(long id, long userId);

    Task<IEnumerable<ToDo>> GetAllByIdAndUserId(long userId, long? categoryId);
}