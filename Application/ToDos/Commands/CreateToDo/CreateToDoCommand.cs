using Application.Abstractions.Messaging;

namespace Application.ToDos.Commands.CreateToDo
{
    public sealed record CreateToDoCommand(
        string Title,
        string Description,
        bool Done,
        long CategoryId
    ) : ICommand<long>;
}