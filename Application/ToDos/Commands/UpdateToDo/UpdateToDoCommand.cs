using Application.Abstractions.Messaging;

namespace Application.ToDos.Commands.UpdateToDo;

public record UpdateToDoCommand(
    string? Title,
    string? Description,
    bool? Done,
    long? CategoryId,
    long Id,
    long UserId) : ICommand<bool>;

public record UpdateToDoRequest(
    string? Title,
    string? Description,
    bool? Done,
    long? CategoryId);