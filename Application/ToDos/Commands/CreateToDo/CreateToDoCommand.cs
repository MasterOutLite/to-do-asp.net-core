using Application.Abstractions.Messaging;
using Application.Common.Models;

namespace Application.ToDos.Commands.CreateToDo;

public sealed record CreateToDoCommand(
    string Title,
    string Description,
    bool Done,
    long CategoryId,
    long UserId
) : ICommand<ToDoResponse>;

public sealed record CreateToDoRequest(
    string Title,
    string Description,
    bool Done,
    long CategoryId);