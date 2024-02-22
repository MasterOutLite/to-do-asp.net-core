using Application.Abstractions.Messaging;
using Application.Common.Models;

namespace Application.ToDos.Queries.GetToDoById;

public sealed record GetToDoByIdQuery(long Id, long UserId) : IQuery<ToDoResponse>;