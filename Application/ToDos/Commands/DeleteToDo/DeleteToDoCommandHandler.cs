using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Domain.Abstractions.Repository;

namespace Application.ToDos.Commands.DeleteToDo;

public class DeleteToDoCommandHandler(
    IToDoRepository toDoRepository,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<DeleteToDoCommand, bool>
{
    public async Task<bool> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var toDo = await toDoRepository.GetByIdAndUserId(request.Id, request.UserId);
        if (toDo is null)
        {
            return false;
        }

        toDoRepository.Remove(toDo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}