using Domain.Entities;

namespace Domain.Abstractions.Repository;

public interface IToDoRepository
{
    void Add(ToDo toDo);
    void Remove(ToDo toDo);
    void Delete(ToDo toDo);
    ToDo? GetById(int id);
}