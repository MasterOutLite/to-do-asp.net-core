using Application.Abstractions.Messaging;

namespace Application.ToDos.Commands.DeleteToDo;

public record DeleteToDoCommand(long Id, long UserId) : ICommand<bool>;

public record DeleteToDoRequest(long Id);