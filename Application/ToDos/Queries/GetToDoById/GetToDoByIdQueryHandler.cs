using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Exceptions.ToDo;
using Mapster;

namespace Application.ToDos.Queries.GetToDoById;

public sealed class GetToDoByIdQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetToDoByIdQuery, ToDoResponse>
{
    public async Task<ToDoResponse> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var toDo = await dbContext.ToDo
            .Where(e => e.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (toDo is null)
        {
            throw new NotFoundToDoException(request.Id);
        }

        return toDo.Adapt<ToDoResponse>();
    }
}