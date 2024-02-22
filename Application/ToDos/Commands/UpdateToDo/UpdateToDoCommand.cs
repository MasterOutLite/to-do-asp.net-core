using Application.Abstractions.Messaging;

namespace Application.ToDos.Commands.UpdateToDo;

public record UpdateToDoCommand : ICommand<bool>;

public record UpdateToDoRequest;