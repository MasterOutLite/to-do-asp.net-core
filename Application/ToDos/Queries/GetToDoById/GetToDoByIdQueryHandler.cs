using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Abstractions.Models;
using Domain.Exceptions;
using Mapster;

namespace Application.ToDos.Queries.GetToDoById;

public sealed class GetToDoByIdQueryHandler : IQueryHandler<GetToDoByIdQuery, ToDoResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetToDoByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ToDoResponse> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var toDo = await _dbContext.ToDo
            .Where(e => e.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (toDo is null)
        {
            throw new NotFoundToDoException(request.Id);
        }

        return toDo.Adapt<ToDoResponse>();
    }
}