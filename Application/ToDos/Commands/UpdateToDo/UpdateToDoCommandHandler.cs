using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Domain.Abstractions.Repository;
using Domain.Entities;
using Domain.Exceptions.ToDo;
using Mapster;
using Serilog;

namespace Application.ToDos.Commands.UpdateToDo;

public class UpdateToDoCommandHandler(
    IToDoRepository toDoRepository,
    IToDoRepositoryQuery toDoRepositoryQuery,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<UpdateToDoCommand, bool>
{
    public async Task<bool> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var toDo = await toDoRepositoryQuery.GetByIdAndUserId(request.Id, request.UserId);
        if (toDo is null)
        {
            throw new NotFoundToDoException(request.Id);
        }

        var config = new TypeAdapterConfig();
        config.NewConfig<UpdateToDoCommand, ToDo>()
            .IgnoreNullValues(true);

        var newToDo = request.Adapt(toDo, config);

        Log.Information("New todo: {@newToDo}. Is repo: {repo}", newToDo, toDoRepository is not null);

        toDoRepository.Update(newToDo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}