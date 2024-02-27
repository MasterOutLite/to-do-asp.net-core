using Domain.Entities;

namespace Domain.Abstractions.Repository;

public interface IToDoRepository
{
    void Add(ToDo toDo);
    void Remove(ToDo toDo);
    void Update(ToDo toDo);

}