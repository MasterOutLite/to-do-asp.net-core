using Application.Abstractions.Interfaces;
using Domain.Abstractions.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ToDo?> GetById(long id)
    {
        return await context.ToDo
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<ToDo?> GetByIdAndUserId(long id, long userId)
    {
        return await context.ToDo
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
    }

    public async Task<IEnumerable<ToDo>> GetAllByIdAndUserId(long userId, long? categoryId)
    {
        return await context.ToDo
            .Where(e => e.UserId == userId && (!categoryId.HasValue || e.CategoryId == categoryId))
            .OrderBy(e => e.Id).ToListAsync();
    }
}