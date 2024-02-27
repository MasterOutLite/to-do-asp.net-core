using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Abstractions.Repository;
using Domain.Exceptions.ToDo;
using Mapster;

namespace Application.ToDos.Queries.GetToDoById;

public sealed class GetToDoByIdQueryHandler(
    IToDoRepositoryQuery toDoRepository
)
    : IQueryHandler<GetToDoByIdQuery, ToDoResponse>
{
    public async Task<ToDoResponse> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var toDo = await toDoRepository.GetByIdAndUserId(request.Id, request.UserId);

        if (toDo is null)
        {
            throw new NotFoundToDoException(request.Id);
        }

        return toDo.Adapt<ToDoResponse>();
    }
}