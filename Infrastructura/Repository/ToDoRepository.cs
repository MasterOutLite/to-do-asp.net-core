using Application.Abstractions.Interfaces;
using Domain.Abstractions.Repository;
using Domain.Entities;

namespace Infrastructure.Repository;

public class ToDoRepository(IApplicationDbContext context) : IToDoRepository
{
    public void Add(ToDo toDo)
    {
        context.ToDo.Add(toDo);
    }

    public void Remove(ToDo toDo)
    {
        context.ToDo.Remove(toDo);
    }

    public void Update(ToDo toDo)
    {
        context.ToDo.Update(toDo);
    }
}