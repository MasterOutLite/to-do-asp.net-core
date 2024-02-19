using Application.Abstractions.Messaging;
using Application.Abstractions.Models;

namespace Application.ToDos.Queries.GetToDoById;

public sealed record GetToDoByIdQuery(long Id) : IQuery<ToDoResponse>;